using System.Collections.ObjectModel;
using System.Windows.Input;
using CrockeryFactory.Desktop.Data;
using CrockeryFactory.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace CrockeryFactory.Desktop.ViewModels;

public class ProductsViewModel : ObservableObject
{
    private Product? _selectedProduct;
    private string _searchText = string.Empty;
    private string _statusMessage = string.Empty;

    public ProductsViewModel()
    {
        NewCommand = new RelayCommand(_ => CreateNew());
        SaveCommand = new RelayCommand(_ => Save(), _ => SelectedProduct is not null);
        DeleteCommand = new RelayCommand(_ => Delete(), _ => SelectedProduct is { Id: > 0 });
        RefreshCommand = new RelayCommand(_ => Load());

        Load();
    }

    public ObservableCollection<Product> Products { get; } = new();

    public IReadOnlyList<ClayBody> ClayBodies { get; } = Enum.GetValues<ClayBody>();

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set => SetProperty(ref _selectedProduct, value);
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                Load();
            }
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public ICommand NewCommand { get; }

    public ICommand SaveCommand { get; }

    public ICommand DeleteCommand { get; }

    public ICommand RefreshCommand { get; }

    private void Load()
    {
        using var db = new FactoryDbContext();

        var query = db.Products.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var term = SearchText.Trim();
            query = query.Where(p => p.Sku.Contains(term) || p.Name.Contains(term));
        }

        Products.Clear();
        foreach (var product in query.OrderBy(p => p.Sku).ToList())
        {
            Products.Add(product);
        }

        StatusMessage = $"{Products.Count} product(s)";
    }

    private void CreateNew()
    {
        var draft = new Product
        {
            Sku = "CUP-NEW-00",
            Name = "New cup",
            ClayBody = ClayBody.Stoneware,
            CapacityMl = 250,
            GlazeColour = "Undecided",
            UnitPrice = 0m
        };

        Products.Add(draft);
        SelectedProduct = draft;
        StatusMessage = "Draft added. Fill in the details, then save.";
    }

    private void Save()
    {
        if (SelectedProduct is null)
        {
            return;
        }

        using var db = new FactoryDbContext();

        if (SelectedProduct.Id == 0)
        {
            db.Products.Add(SelectedProduct);
        }
        else
        {
            db.Products.Update(SelectedProduct);
        }

        db.SaveChanges();
        StatusMessage = $"Saved {SelectedProduct.Sku}.";
        Load();
    }

    private void Delete()
    {
        if (SelectedProduct is null or { Id: 0 })
        {
            return;
        }

        using var db = new FactoryDbContext();
        var tracked = db.Products.Find(SelectedProduct.Id);

        if (tracked is not null)
        {
            db.Products.Remove(tracked);
            db.SaveChanges();
            StatusMessage = $"Deleted {tracked.Sku}.";
        }

        SelectedProduct = null;
        Load();
    }
}
