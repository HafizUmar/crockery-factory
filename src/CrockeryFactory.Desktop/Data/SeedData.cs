using CrockeryFactory.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace CrockeryFactory.Desktop.Data;

public static class SeedData
{
    /// <summary>Creates the database on first run and fills it with a plausible catalogue.</summary>
    public static void EnsureSeeded()
    {
        using var db = new FactoryDbContext();
        db.Database.EnsureCreated();

        if (db.Products.Any())
        {
            return;
        }

        var products = new List<Product>
        {
            new() { Sku = "CUP-ESP-01", Name = "Ristretto espresso cup, 60 ml", ClayBody = ClayBody.Porcelain,  CapacityMl = 60,  GlazeColour = "Bone white",   UnitPrice = 3.20m,  StockQuantity = 1840 },
            new() { Sku = "CUP-CAP-02", Name = "Cappuccino cup, 180 ml",       ClayBody = ClayBody.Porcelain,  CapacityMl = 180, GlazeColour = "Bone white",   UnitPrice = 4.75m,  StockQuantity = 960  },
            new() { Sku = "MUG-DIN-03", Name = "Diner mug, 340 ml",            ClayBody = ClayBody.Stoneware,  CapacityMl = 340, GlazeColour = "Slate grey",   UnitPrice = 6.10m,  StockQuantity = 2450 },
            new() { Sku = "MUG-CAM-04", Name = "Campfire mug, 400 ml",         ClayBody = ClayBody.Stoneware,  CapacityMl = 400, GlazeColour = "Rust",         UnitPrice = 7.40m,  StockQuantity = 315  },
            new() { Sku = "CUP-TEA-05", Name = "Teacup and saucer, 200 ml",    ClayBody = ClayBody.BoneChina,  CapacityMl = 200, GlazeColour = "Cobalt band",  UnitPrice = 11.90m, StockQuantity = 480  },
            new() { Sku = "MUG-PRO-06", Name = "Promotional mug, 300 ml",      ClayBody = ClayBody.Earthenware,CapacityMl = 300, GlazeColour = "Blank white",  UnitPrice = 2.85m,  StockQuantity = 7300 },
            new() { Sku = "CUP-CHA-07", Name = "Chai cutting glass cup, 90 ml",ClayBody = ClayBody.Earthenware,CapacityMl = 90,  GlazeColour = "Terracotta",   UnitPrice = 1.95m,  StockQuantity = 5120 },
            new() { Sku = "MUG-HER-08", Name = "Heritage mug, 350 ml",         ClayBody = ClayBody.Stoneware,  CapacityMl = 350, GlazeColour = "Celadon",      UnitPrice = 8.60m,  StockQuantity = 0, IsDiscontinued = true }
        };

        db.Products.AddRange(products);
        db.SaveChanges();

        var today = DateTime.Today;
        var batches = new List<ProductionBatch>
        {
            new() { BatchCode = "B-2609-014", ProductId = products[2].Id, QuantityPlanned = 1200, QuantityGood = 1163, QuantityScrapped = 37,  Status = BatchStatus.Completed,    KilnId = "KILN-2", StartedOn = today.AddDays(-21), CompletedOn = today.AddDays(-18) },
            new() { BatchCode = "B-2609-015", ProductId = products[5].Id, QuantityPlanned = 3000, QuantityGood = 2874, QuantityScrapped = 126, Status = BatchStatus.Completed,    KilnId = "KILN-1", StartedOn = today.AddDays(-17), CompletedOn = today.AddDays(-13) },
            new() { BatchCode = "B-2609-016", ProductId = products[4].Id, QuantityPlanned = 600,  QuantityGood = 512,  QuantityScrapped = 88,  Status = BatchStatus.Completed,    KilnId = "KILN-3", StartedOn = today.AddDays(-12), CompletedOn = today.AddDays(-8)  },
            new() { BatchCode = "B-2609-017", ProductId = products[3].Id, QuantityPlanned = 800,  QuantityGood = 0,    QuantityScrapped = 0,   Status = BatchStatus.GlostFiring,  KilnId = "KILN-2", StartedOn = today.AddDays(-4) },
            new() { BatchCode = "B-2609-018", ProductId = products[0].Id, QuantityPlanned = 2000, QuantityGood = 0,    QuantityScrapped = 0,   Status = BatchStatus.Glazing,      KilnId = "KILN-1", StartedOn = today.AddDays(-2) },
            new() { BatchCode = "B-2609-019", ProductId = products[1].Id, QuantityPlanned = 1500, QuantityGood = 0,    QuantityScrapped = 0,   Status = BatchStatus.Forming,      KilnId = "",       StartedOn = today.AddDays(-1) },
            new() { BatchCode = "B-2609-020", ProductId = products[6].Id, QuantityPlanned = 4000, QuantityGood = 0,    QuantityScrapped = 0,   Status = BatchStatus.Planned,      KilnId = "",       StartedOn = today.AddDays(3) }
        };

        db.Batches.AddRange(batches);
        db.SaveChanges();
    }
}
