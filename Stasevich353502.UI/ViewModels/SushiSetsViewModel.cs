using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stasevich353502.Application.SushiSetUseCases.Queries;
using Stasevich353502.Application.SushiUseCases.Queries;
using Stasevich353502.UI.Pages;

namespace Stasevich353502.UI.ViewModels;

public partial class SushiSetsViewModel(IMediator mediator) : ObservableObject
{
    public ObservableCollection<SushiSet> SushiSets { get; set; } = new();
    public ObservableCollection<Sushi> Sushi { get; set; } = new();
    
    partial void OnSelectedSushiSetChanged(SushiSet oldValue, SushiSet newValue)
    { 
        Sushi.Clear();
    }
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(UpdateSushiListCommand))]
    SushiSet _selectedSushiSet;

    [RelayCommand]
    async Task UpdateSushiSetsList() => await GetSushiSets();
    
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
    }

    [RelayCommand(CanExecute = nameof(CanUpdateSushiList))]
    async Task UpdateSushiList() => await GetSushi();
    
    private bool CanUpdateSushiList()
    {
        return _selectedSushiSet != null;
    }

    public async Task GetSushi()
    {
        Sushi.Clear();
        var sushi = await mediator.Send(new GetSushiBySetIdQuery(_selectedSushiSet.Id));
        if (sushi != null)
        {
            foreach (var sushiItem in sushi)
            {
                Sushi.Add(sushiItem);
            }
        }
    }
    
    [RelayCommand]
    async Task ShowDetails(Sushi sushi) => await GotoDetailsPage(sushi);
    
    private async Task GotoDetailsPage(Sushi sushi)
    {
        IDictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "SelectedSushiObject", sushi }
        };

        await Shell.Current.GoToAsync(nameof(SushiDetailsPage), parameters);
    }

    [RelayCommand]
    async Task CreateSushiSet()
    {
        await GoToCreateSushiSetPage();
    }

    private async Task GoToCreateSushiSetPage()
    {
        await Shell.Current.GoToAsync(nameof(CreateSushiSetPage));
    }

    [RelayCommand]
    async Task CreateSushiInSushiSet()
    {
        await GoToCreateSushiInSushiSetPage();
    }

    private async Task GoToCreateSushiInSushiSetPage()
    {
        if(_selectedSushiSet == null)
            return;
        IDictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "SelectedSushiSetObject", SelectedSushiSet }
        };
        await Shell.Current.GoToAsync(nameof(CreateSushiInSushiSetPage), parameters);
    }
}