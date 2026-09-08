namespace CrockeryFactory.Desktop.ViewModels;

public class MainViewModel : ObservableObject
{
    public ProductsViewModel Products { get; } = new();

    public ProductionViewModel Production { get; } = new();
}
