using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using my = Resources.Classes;

namespace MauiApp1.ViewModel
{
    [QueryProperty("Place", "Place")]
    public partial class PlaceDetailsViewModel : BaseViewModel
    {
        LocationService locationService;

        public PlaceDetailsViewModel(LocationService locationService)
        {
            this.locationService = locationService;
        }

        [ObservableProperty]
        my.Place place;


        [ObservableProperty]
        string editTitle;
        [ObservableProperty]
        string editSubCategory;
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
                if (string.IsNullOrWhiteSpace(EditTitle) || string.IsNullOrWhiteSpace(EditSubCategory) || string.IsNullOrWhiteSpace(EditDescription))
                {
                    await Shell.Current.DisplayAlert("Error!", "Please fill out all fields", "OK");
                    return;
                }
                bool answer = await Shell.Current.DisplayAlert("Save editing of " + "\"" + Place.Title + "\"?", "Are you sure?", "Save", "Cancel");
                if (!answer)
                    return;

                Place.Title = EditTitle;
                Place.SubCategory = EditSubCategory;
                Place.Description = EditDescription;
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
        }
    }
}
