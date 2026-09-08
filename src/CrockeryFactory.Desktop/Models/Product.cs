namespace CrockeryFactory.Desktop.Models;

/// <summary>A cup the factory makes and sells, identified by SKU.</summary>
public class Product
{
    public int Id { get; set; }

    public string Sku { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public ClayBody ClayBody { get; set; }

    /// <summary>Brimful capacity in millilitres.</summary>
    public int CapacityMl { get; set; }

    public string GlazeColour { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    /// <summary>Finished cups currently in the warehouse.</summary>
    public int StockQuantity { get; set; }

    public bool IsDiscontinued { get; set; }

    public List<ProductionBatch> Batches { get; set; } = new();

    public override string ToString() => $"{Sku} - {Name}";
}
