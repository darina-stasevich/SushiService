using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stasevich353502.UI.ViewModels;

namespace Stasevich353502.UI.Pages;

public partial class SushiDetailsPage : ContentPage
{
    public SushiDetailsPage(SushiDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SushiDetailsViewModel viewModel)
        {
            await viewModel.LoadFreshSushiDataAsync();
        }
    }
}