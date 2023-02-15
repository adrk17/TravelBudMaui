using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class PlaceDetailsPage : ContentPage
{
	public PlaceDetailsPage(PlaceDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}