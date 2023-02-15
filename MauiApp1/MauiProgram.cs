using CommunityToolkit.Maui;
using MauiApp1.Services;
using MauiApp1.ViewModel;

namespace MauiApp1;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();

        builder.Services.AddSingleton<LocationService>();
        
        builder.Services.AddSingleton<LocationViewModel>();
        builder.Services.AddTransient<LocationDetailsViewModel>();
        builder.Services.AddSingleton<AddLocationViewModel>();
        builder.Services.AddTransient<AddPlaceViewModel>();
        builder.Services.AddTransient<PlaceDetailsViewModel>();
        
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<LocationDetailsPage>();
        builder.Services.AddSingleton<AddLocationPage>();
        builder.Services.AddTransient<AddPlacePage>();
        builder.Services.AddTransient<PlaceDetailsPage>();

        return builder.Build();
    }
}