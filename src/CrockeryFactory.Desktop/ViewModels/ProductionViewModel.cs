using System.Collections.ObjectModel;
using System.Windows.Input;
using CrockeryFactory.Desktop.Data;
using CrockeryFactory.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace CrockeryFactory.Desktop.ViewModels;

public class ProductionViewModel : ObservableObject
{
    private ProductionBatch? _selectedBatch;
    private string _statusMessage = string.Empty;

    public ProductionViewModel()
    {
        AdvanceCommand = new RelayCommand(_ => AdvanceStage(), _ => CanAdvance());
        RefreshCommand = new RelayCommand(_ => Load());

        Load();
    }

    public ObservableCollection<ProductionBatch> Batches { get; } = new();

    public ProductionBatch? SelectedBatch
    {
        get => _selectedBatch;
        set => SetProperty(ref _selectedBatch, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public ICommand AdvanceCommand { get; }

    public ICommand RefreshCommand { get; }

    private void Load()
    {
        using var db = new FactoryDbContext();

        var rows = db.Batches
            .AsNoTracking()
            .Include(b => b.Product)
            .OrderByDescending(b => b.StartedOn)
            .ToList();

        Batches.Clear();
        foreach (var batch in rows)
        {
            Batches.Add(batch);
        }

        var open = rows.Count(b => b.Status is not (BatchStatus.Completed or BatchStatus.Scrapped));
        StatusMessage = $"{rows.Count} batch(es), {open} still on the floor";
    }

    private bool CanAdvance()
        => SelectedBatch is not null
           && SelectedBatch.Status is not (BatchStatus.Completed or BatchStatus.Scrapped);

    /// <summary>
    /// Moves a batch to the next stage. On the final stage it books the good cups into stock.
    /// The good/scrap split is a placeholder - see PRACTICE-TASKS.md, task 3.
    /// </summary>
    private void AdvanceStage()
    {
        if (SelectedBatch is null)
        {
            return;
        }

        using var db = new FactoryDbContext();

        var batch = db.Batches.Include(b => b.Product).FirstOrDefault(b => b.Id == SelectedBatch.Id);
        if (batch is null)
        {
            return;
        }

        var next = batch.Status switch
        {
            BatchStatus.Planned => BatchStatus.Forming,
            BatchStatus.Forming => BatchStatus.BisqueFiring,
            BatchStatus.BisqueFiring => BatchStatus.Glazing,
            BatchStatus.Glazing => BatchStatus.GlostFiring,
            BatchStatus.GlostFiring => BatchStatus.Completed,
            _ => batch.Status
        };

        batch.Status = next;

        if (next == BatchStatus.Completed)
        {
            batch.QuantityGood = batch.QuantityPlanned;
            batch.QuantityScrapped = 0;
            batch.CompletedOn = DateTime.Today;

            if (batch.Product is not null)
            {
                batch.Product.StockQuantity += batch.QuantityGood;
            }
        }

        db.SaveChanges();
        StatusMessage = $"{batch.BatchCode} moved to {next}.";
        Load();
    }
}
