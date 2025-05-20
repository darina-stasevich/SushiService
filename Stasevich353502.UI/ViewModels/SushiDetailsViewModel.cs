using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stasevich353502.Application.SushiUseCases.Commands;

namespace Stasevich353502.UI.ViewModels;

[QueryProperty(nameof(CurrentSushi), "SelectedSushiObject")]
public partial class SushiDetailsViewModel(IMediator mediator) : ObservableObject
{
    [ObservableProperty]
    private Sushi currentSushi;
    
    [RelayCommand]
    async Task DeleteSushi() => await DeleteSushiFromSet();

    private async Task DeleteSushiFromSet()
    {
        try
        {
            await mediator.Send(new DeleteSushiCommand(currentSushi));
            await Shell.Current.DisplayAlert("Успех", "Суши удален из сета", "Ок");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", $"{ex.Message}", "Ок");
            await Shell.Current.GoToAsync("..");
        }
    }
}