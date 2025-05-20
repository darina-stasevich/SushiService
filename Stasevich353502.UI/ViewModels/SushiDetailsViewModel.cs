using CommunityToolkit.Mvvm.ComponentModel;

namespace Stasevich353502.UI.ViewModels;

[QueryProperty(nameof(CurrentSushi), "SelectedSushiObject")]
public partial class SushiDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private Sushi currentSushi;

    public SushiDetailsViewModel()
    {
    }
}