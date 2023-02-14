using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using my = Resources.Classes;
namespace MauiApp1.ViewModel
{
    [QueryProperty("Location", "Location")]
    public partial class LocationDetailsViewModel : BaseViewModel
    {
        LocationService locationService;
        
        public LocationDetailsViewModel(LocationService locationService)
        {
            this.locationService = locationService;
        }

        [ObservableProperty]
        my.Location location;

        [RelayCommand]
        void Save()
        {
            locationService.SaveLocations();
        }

        [RelayCommand]
        async Task GoToAddPlaceAsync()
        {
            if (Location is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(AddPlacePage)}", true,
                new Dictionary<string, object>
                {
                    {"Location", Location}
                });
        }

        
    }

   
}
