using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stasevich353502.Application.SushiSetUseCases.Commands;
using Stasevich353502.Application.SushiUseCases.Commands;

namespace Stasevich353502.UI.ViewModels;

[QueryProperty(nameof(CurrentSet), "SelectedSushiSetObject")]
public partial class CreateSushiInSushiSetViewModel(IMediator mediator) : ObservableObject
{
    [ObservableProperty]
    private SushiSet currentSet;

    [ObservableProperty]
    string? sushiType;

    [ObservableProperty]
    string? name;

    [ObservableProperty] 
    string? amount;
    
    [RelayCommand]
    async Task AddSushiToSushiSet() => await AddSushi();

    private async Task AddSushi()
    {
        bool nameValidationResult = await ValidateType(SushiType);
        bool priceValidationResult = await ValidateName(Name);
        bool weightValidationResult = await ValidateAmount(Amount);

        if (nameValidationResult & priceValidationResult & weightValidationResult)
        {
            var sushi = await mediator.Send(new AddSushiCommand(new SushiData(name, sushiType), Int32.Parse(amount), currentSet.Id));
            if (sushi != null)
            {
                await Shell.Current.DisplayAlert("Успех", $"Суши {sushi.SushiData.SushiType} {sushi.SushiData.SushiName} успешно добавлен в сет {currentSet.Name}", "Ок");
                SushiType = String.Empty;
                Name = String.Empty;
                Amount = String.Empty;

                await Shell.Current.GoToAsync("..");
            }
        }
    }

    private async Task<bool> ValidateName(string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Название не должно быть пустым", "Ок");
            return false;
        }

        return true;
    }
    
    private async Task<bool> ValidateType(string? type)
    {
        if (string.IsNullOrEmpty(type))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Тип не должен быть пустым", "Ок");
            return false;
        }
        return true;
    }
    
    private async Task<bool> ValidateAmount(string? amount)
    {
        if (string.IsNullOrEmpty(amount))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Количество должно быть указано", "Ок");
            return false;
        }
        var parseResult = Int32.TryParse(amount, out var currentAmount);
        if (!parseResult)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Введите количество в корректном формате", "Ок");
            return false;
        }
        if (currentAmount <= 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Введите корректное количество", "Ок");
            return false;
        }
        
        return true;
    }
    
}