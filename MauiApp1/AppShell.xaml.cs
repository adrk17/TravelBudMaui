namespace MauiApp1;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(LocationDetailsPage), typeof(LocationDetailsPage));
        Routing.RegisterRoute(nameof(AddLocationPage), typeof(AddLocationPage));
        Routing.RegisterRoute(nameof(AddPlacePage), typeof(AddPlacePage));
        Routing.RegisterRoute(nameof(PlaceDetailsPage), typeof(PlaceDetailsPage));
    }
}
