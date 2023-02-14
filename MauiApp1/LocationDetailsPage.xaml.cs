using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class LocationDetailsPage : ContentPage
{
	public LocationDetailsPage(LocationDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

   
}