using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stasevich353502.Application.SushiUseCases.Commands;
using Stasevich353502.Application.SushiUseCases.Queries;
using Stasevich353502.UI.Pages;

namespace Stasevich353502.UI.ViewModels;

[QueryProperty(nameof(CurrentSushi), "SelectedSushiObject")]
public partial class SushiDetailsViewModel(IMediator mediator) : ObservableObject
{
    [ObservableProperty]
    private Sushi _currentSushi;
    
    [RelayCommand]
    async Task DeleteSushi() => await DeleteSushiFromSet();

    private async Task DeleteSushiFromSet()
    {
        try
        {
            await mediator.Send(new DeleteSushiCommand(_currentSushi));
            await Shell.Current.DisplayAlert("Успех", "Суши удален из сета", "Ок");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", $"{ex.Message}", "Ок");
        }
        finally
        {
            await Shell.Current.GoToAsync(".."); 
        }
    }
    
    [RelayCommand]
    async Task UpdateSushi() => await GoToUpdateSushiPage();
    
    private async Task GoToUpdateSushiPage()
    {
        if (_currentSushi == null)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Суши не выбрано", "Ок");
            return;
        }
        try
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "SelectedSushiObject", _currentSushi }
            };
            await Shell.Current.GoToAsync($"{nameof(UpdateSushiPage) }", parameters);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", $"{ex.Message}", "Ок");
            await Shell.Current.GoToAsync("..");
        }
    }
    
    public async Task LoadFreshSushiDataAsync()
    {
        if (CurrentSushi == null || CurrentSushi.Id == Guid.Empty)
        {
           return;
        }

        try
        {
            var freshSushi = await mediator.Send(new GetSushiByIdQuery(CurrentSushi.Id));
            if (freshSushi != null)
            {
                CurrentSushi = freshSushi;
                OnPropertyChanged(nameof(CurrentSushi));
            }
            else
            {
                await Shell.Current.DisplayAlert("Не найдено",
                    "Данные для этого суши не найдены. Возможно, оно было удалено.", "Ок");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка загрузки", $"Не удалось обновить детали суши: {ex.Message}", "Ок");
            await Shell.Current.GoToAsync("..");
        }
    }
}