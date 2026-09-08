using System.ComponentModel.DataAnnotations.Schema;

namespace CrockeryFactory.Desktop.Models;

/// <summary>One run of a single product through the kilns.</summary>
public class ProductionBatch
{
    public int Id { get; set; }

    public string BatchCode { get; set; } = string.Empty;

    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public int QuantityPlanned { get; set; }

    /// <summary>Cups that passed inspection.</summary>
    public int QuantityGood { get; set; }

    /// <summary>Cups lost to cracks, glaze faults or warping.</summary>
    public int QuantityScrapped { get; set; }

    public BatchStatus Status { get; set; }

    public string KilnId { get; set; } = string.Empty;

    public DateTime StartedOn { get; set; }

    public DateTime? CompletedOn { get; set; }

    [NotMapped]
    public int QuantityFired => QuantityGood + QuantityScrapped;

    /// <summary>Share of fired cups that had to be scrapped, 0 to 1.</summary>
    [NotMapped]
    public double ScrapRate => QuantityFired == 0 ? 0d : (double)QuantityScrapped / QuantityFired;
}
