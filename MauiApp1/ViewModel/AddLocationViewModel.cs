using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using static System.Net.WebRequestMethods;
using my = Resources.Classes;

namespace MauiApp1.ViewModel
{
    public partial class AddLocationViewModel : BaseViewModel
    {
        LocationService locationService;
        public AddLocationViewModel(LocationService locationService)
        {
            this.locationService = locationService;
            this.Title = "Add a new Location";
        }

        [ObservableProperty]
        string locationName;

        [ObservableProperty]
        string description;
        
        [ObservableProperty]
        string country;


        [RelayCommand]
        async Task AddNewLocationAsync()
        {
            if (string.IsNullOrWhiteSpace(LocationName) || string.IsNullOrWhiteSpace(Country))
            {
                await Shell.Current.DisplayAlert("Error!", "Please fill out all fields", "OK");
                return;
            }
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                if (string.IsNullOrWhiteSpace(Description))
                {
                    Description = "";
                }
                if (string.IsNullOrWhiteSpace(ImgUrl))
                {
                    ImgUrl = "";
                }
                my.Location newLocation = new my.Location(LocationName, Description, Country, null, ImgUrl);
                locationService.AddLocation(newLocation);
                await Shell.Current.GoToAsync("..");
            }
            catch(Exception ex)
             {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to add a location: {ex.Message}", "OK");

             }
            finally
            {
                IsBusy = false;
            }
        }

        [ObservableProperty]
        ImageSource imageSourceVar;

        [ObservableProperty]
        string imgUrl;

        [RelayCommand]
        public void PickImage()
        {
            if (ImgUrl is null || ImgUrl == "")
            {
                return;
            }
            try
            {
                ImageSourceVar = ImageSource.FromUri(new Uri(ImgUrl));
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
    
}
