using System.Collections.ObjectModel;
using System.Formats.Asn1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stasevich353502.Application.SushiSetUseCases.Queries;
using Stasevich353502.Application.SushiUseCases.Commands;

namespace Stasevich353502.UI.ViewModels;

[QueryProperty(nameof(CurrentSushi), "SelectedSushiObject")]
public partial class UpdateSushiViewModel(IMediator mediator) : ObservableObject
{
    [ObservableProperty]
    private Sushi _currentSushi;

    [ObservableProperty]
    string? _sushiType;

    [ObservableProperty]
    string? _name;

    [ObservableProperty] 
    string? _amount;

    [ObservableProperty]
    SushiSet _selectedSushiSet;
    public ObservableCollection<SushiSet> SushiSets { get; set; } = new();

    public async Task GetSushiSets()
    {
        var sushiSets = await mediator.Send(new GetAllSushiSetsQuery());
        if (sushiSets != null)
        {
            SushiSets.Clear();
            foreach (var sushiSet in sushiSets)
            {
                SushiSets.Add(sushiSet);
            }
        }

        SelectedSushiSet = SushiSets.First(x => x.Id == _currentSushi.SushiSetId);
        
    }
    
    async partial void OnCurrentSushiChanged(Sushi value)
    {
        SushiType = value.SushiData.SushiType;
        Name = value.SushiData.SushiName;
        Amount = value.Amount.ToString();

        await GetSushiSets();
    }

    [RelayCommand]
    async Task UpdateSushi() => await UpdateCurrentSushi();

    private async Task UpdateCurrentSushi()
    {
        bool nameValidationResult = await ValidateName(Name);
        bool typeValidationResult = await ValidateType(SushiType);
        bool amountValidationResult = await ValidateAmount(Amount);
        if(nameValidationResult && typeValidationResult && amountValidationResult == false)
        {
            return;
        }
        try
        {
            if (_selectedSushiSet.Id != CurrentSushi.SushiSetId)
            {
                await mediator.Send(new ChangeSushiSetCommand(CurrentSushi, _selectedSushiSet.Id));
            }

            await mediator.Send(new UpdateSushiCommand(_currentSushi.Id, SushiType, Name, Int32.Parse(Amount)));
            await Shell.Current.DisplayAlert("Успех", "Изменено успешно", "Ок");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", $"{ex.Message}", "Ок");
        }
        finally
        {
            await Shell.Current.GoToAsync("../");
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