using System.Windows;
using CrockeryFactory.Desktop.Data;

namespace CrockeryFactory.Desktop;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        SeedData.EnsureSeeded();
    }
}
