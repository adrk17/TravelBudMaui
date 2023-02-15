using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using static Android.Provider.CallLog;
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
        [ObservableProperty]
        string editTitle;
        [ObservableProperty]
        string editCountry;
        [ObservableProperty]
        string editDescription;

        [RelayCommand]
        async Task SaveEditChangesAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                if (string.IsNullOrWhiteSpace(EditTitle) || string.IsNullOrWhiteSpace(EditCountry) || string.IsNullOrWhiteSpace(EditDescription))
                {
                    await Shell.Current.DisplayAlert("Error!", "Please fill out all fields", "OK");
                    return;
                }
                bool answer = await Shell.Current.DisplayAlert("Save editing of " + "\"" + Location.Title + "\"?", "Are you sure?", "Save", "Cancel");
                if (!answer)
                    return;

                Location.Title = EditTitle;
                Location.Country = EditCountry;
                Location.Description = EditDescription;
                locationService.SaveLocations();
                await Shell.Current.GoToAsync("../");

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to edit the location: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        void LoadEditValues()
        {
            EditTitle = Location.Title;
            EditCountry = Location.Country;
            EditDescription = Location.Description;
        }


        [RelayCommand]
        async Task GoToPlaceDetailsAsync(my.Place place)
        {
            if (place is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(PlaceDetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {"Place", place}
                });
        }

        [RelayCommand]
        async Task DeletePlaceAsync(my.Place place)
        {
            bool answer = await Shell.Current.DisplayAlert("Delete " + "\"" + place.Title + "\"?", "Are you sure?", "Delete", "Cancel");
            if (!answer)
            {
                return;
            }

            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                Location.Places.Remove(place);
                await Shell.Current.GoToAsync("../");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to delete place: {ex.Message}", "OK");

            }
            finally
            {
                IsBusy = false;
            }
        }
    }

   
}
