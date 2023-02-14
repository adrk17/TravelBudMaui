using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class AddPlacePage : ContentPage
{
    
	public AddPlacePage(AddPlaceViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}