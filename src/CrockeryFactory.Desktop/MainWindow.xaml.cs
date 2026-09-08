using System.Windows;
using CrockeryFactory.Desktop.ViewModels;

namespace CrockeryFactory.Desktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
