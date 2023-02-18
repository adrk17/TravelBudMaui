using Android.Webkit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliJ.Lang.Annotations;
using MauiApp1.Services;
using Microsoft.Maui.Devices.Sensors;
using my = Resources.Classes;

namespace MauiApp1.ViewModel
{
    [QueryProperty("Place", "Place")]
    public partial class PlaceDetailsViewModel : BaseViewModel
    {
        LocationService locationService;
        IGeolocation geolocation;
        public PlaceDetailsViewModel(LocationService locationService, IGeolocation geolocation)
        {
            this.locationService = locationService;
            this.geolocation = geolocation;
        }

        [ObservableProperty]
        my.Place place;


        [ObservableProperty]
        string editTitle;
        [ObservableProperty]
        string editSubCategory;
        [ObservableProperty]
        string editDescription;
        [ObservableProperty]
        string editImgUrl;

        [RelayCommand]
        async Task SaveEditChangesAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                if (string.IsNullOrWhiteSpace(EditTitle) || string.IsNullOrWhiteSpace(EditSubCategory))
                {
                    await Shell.Current.DisplayAlert("Error!", "Please fill out all fields", "OK");
                    return;
                }
                if (string.IsNullOrWhiteSpace(EditDescription))
                {
                    EditDescription = "";
                }
                if (string.IsNullOrWhiteSpace(EditImgUrl))
                {
                    EditImgUrl = "";
                }
                bool answer = await Shell.Current.DisplayAlert("Save editing of " + "\"" + Place.Title + "\"?", "Are you sure?", "Save", "Cancel");
                if (!answer)
                    return;

                Place.Title = EditTitle;
                Place.SubCategory = EditSubCategory;
                Place.Description = EditDescription;
                Place.ImageURL = EditImgUrl;
                locationService.SaveLocations();
                await Shell.Current.GoToAsync("../../");

            }
            catch (Exception ex)
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
            EditTitle = Place.Title;
            EditSubCategory = Place.SubCategory;
            EditDescription = Place.Description;
            EditImgUrl = Place.ImageURL;
        }

     

       


        [ObservableProperty]
        bool coords;
        [ObservableProperty]
        bool address;
        [ObservableProperty]
        bool addressCategory = true;


        [RelayCommand]
        public async Task NavigateToMaps()
        {
            try
            {
                if (Coords)
                {
                    await Map.OpenAsync(Place.Latitude, Place.Longitude);
                }
                else if (Address)
                {
                    var options = new MapLaunchOptions { Name = Place.Title + " " + Place.City + " " + Place.Country };
                    Placemark pl = new Placemark();
                    pl.Location = new Location(Place.Latitude, Place.Longitude);
                    await Map.OpenAsync(pl, options);
                }
                else if (AddressCategory)
                {
                    var options = new MapLaunchOptions { Name = Place.Title + " " + Place.SubCategory + " " + Place.City + " " + Place.Country };
                    Placemark pl = new Placemark();
                    pl.Location = new Location(Place.Latitude, Place.Longitude);
                    await Map.OpenAsync(pl, options);
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to open maps: {ex.Message}", "OK");
            }
        }


        [RelayCommand]
        async Task OpenBrowser()
        {
            try
            {
                Uri uri = new Uri("https://www.google.com/search?q=" + "+" + Place.Title + "+" + Place.SubCategory + "+" + Place.City + "+" + Place.Country);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to open browser: {ex.Message}", "OK");
            }
        }
    }
}
