using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class AddLocationPage : ContentPage
{
	public AddLocationPage(AddLocationViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}