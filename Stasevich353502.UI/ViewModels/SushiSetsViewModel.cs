using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stasevich353502.Application.SushiSetUseCases.Queries;
using Stasevich353502.Application.SushiUseCases.Queries;

namespace Stasevich353502.UI.ViewModels;

public partial class SushiSetsViewModel(IMediator mediator) : ObservableObject
{
    public ObservableCollection<SushiSet> SushiSets { get; set; } = new();
    public ObservableCollection<Sushi> Sushi { get; set; } = new();
    
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
        var sushi = await mediator.Send(new GetSushiBySetIdQuery(_selectedSushiSet.Id));
        if (sushi != null)
        {
            Sushi.Clear();
            foreach (var sushiItem in sushi)
            {
                Sushi.Add(sushiItem);
            }
        }
    }
}