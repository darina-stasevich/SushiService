using System.Security.AccessControl;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stasevich353502.Application.SushiSetUseCases.Commands;

namespace Stasevich353502.UI.ViewModels;

public partial class CreateSushiSetViewModel(IMediator mediator) : ObservableObject
{
    [ObservableProperty]
    string? name;

    [ObservableProperty]
    string? price;

    [ObservableProperty] 
    string? weight;
    
    [RelayCommand]
    async Task CreateSushiSet() => await AddSushiSet();

    private async Task AddSushiSet()
    {
        bool nameValidationResult = await ValidateName(Name);
        bool priceValidationResult = await ValidatePrice(Price);
        bool weightValidationResult = await ValidateWeight(Weight);

        if (nameValidationResult && priceValidationResult && weightValidationResult)
        {
            var set = await mediator.Send(new AddSushiSetCommand(name, Decimal.Parse(price), Decimal.Parse(weight)));
            if (set != null)
            {
                await Shell.Current.DisplayAlert("Успех", $"Суши сет {set.Name} успешно создан", "Ок");
                Name = String.Empty;
                Price = String.Empty;
                Weight = String.Empty;

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
    
    private async Task<bool> ValidatePrice(string? price)
    {
        if (string.IsNullOrEmpty(price))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Цена должна быть указана", "Ок");
            return false;
        }
        var parseResult = Decimal.TryParse(price, out var currentPrice);
        if (!parseResult)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Введите цену в корректном формате", "Ок");
            return false;
        }
        if (currentPrice <= 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Цена не должна быть отрицательной", "Ок");
            return false;
        }
        
        return true;
    }
    
    private async Task<bool> ValidateWeight(string? weight)
    {
        if (string.IsNullOrEmpty(weight))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Вес должен быть указан", "Ок");
            return false;
        }
        var parseResult = Decimal.TryParse(weight, out var currentWeight);
        if (!parseResult)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Введите вес в корректном формате", "Ок");
            return false;
        }
        if (currentWeight <= 5 || currentWeight > 3000)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Введите корректный вес", "Ок");
            return false;
        }
        
        return true;
    }
    
}