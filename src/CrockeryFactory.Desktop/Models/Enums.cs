namespace CrockeryFactory.Desktop.Models;

/// <summary>Clay body a cup is thrown from. Determines firing temperature and scrap rate.</summary>
public enum ClayBody
{
    Earthenware,
    Stoneware,
    Porcelain,
    BoneChina
}

/// <summary>Stages a production batch moves through on the shop floor.</summary>
public enum BatchStatus
{
    Planned,
    Forming,
    BisqueFiring,
    Glazing,
    GlostFiring,
    Completed,
    Scrapped
}
