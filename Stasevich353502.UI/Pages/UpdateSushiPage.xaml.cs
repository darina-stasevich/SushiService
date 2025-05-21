using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stasevich353502.UI.ViewModels;

namespace Stasevich353502.UI.Pages;

public partial class UpdateSushiPage : ContentPage
{
    public UpdateSushiPage(UpdateSushiViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}