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
