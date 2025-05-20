using Stasevich353502.UI.Pages;

namespace Stasevich353502.UI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(SushiDetailsPage), typeof(SushiDetailsPage));
    }
}