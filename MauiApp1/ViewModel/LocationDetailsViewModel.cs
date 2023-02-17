using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using Resources.Classes;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
                if (string.IsNullOrWhiteSpace(EditTitle) || string.IsNullOrWhiteSpace(EditCountry))
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
                bool answer = await Shell.Current.DisplayAlert("Save editing of " + "\"" + Location.Title + "\"?", "Are you sure?", "Save", "Cancel");
                if (!answer)
                    return;

                Location.Title = EditTitle;
                Location.Country = EditCountry;
                Location.Description = EditDescription;
                Location.ImageURL = EditImgUrl;
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
            EditImgUrl = Location.ImageURL;
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

        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>{ "Show Everything" };

        [RelayCommand]
        void FindCategories()
        {
            foreach (my.Place place in Location.Places)
            {
                if (!Categories.Contains(place.SubCategory)){
                    Categories.Add(place.SubCategory);
                }

            }
        }

        [ObservableProperty]
        string selectedCategory;

        partial void OnSelectedCategoryChanged(string value)
        {
            foreach(my.Place place in Location.Places)
            {
                if(value == place.SubCategory || value == "Show Everything")
                {
                    place.IsVisible = true;
                }
                else
                {
                    place.IsVisible = false;
                }
            }
            Location.Places.Sort();
        }

        [RelayCommand]
        void ShowEveryPlace()
        {
            foreach (my.Place place in Location.Places)
            {
                place.IsVisible = true;
            }
            Location.Places.Sort();
        }


        [ObservableProperty]
        ImageSource editImageSourceVar;

        [RelayCommand]
        public void EditPickImage()
        {
            if (EditImgUrl is null || EditImgUrl == "")
            {
                return;
            }
            try
            {
                EditImageSourceVar = ImageSource.FromUri(new Uri(EditImgUrl));
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }

    
}
