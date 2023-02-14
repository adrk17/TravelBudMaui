using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
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
            if (string.IsNullOrWhiteSpace(LocationName) || string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(Country))
            {
                await Shell.Current.DisplayAlert("Error!", "Please fill out all fields", "OK");
                return;
            }
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                my.Location newLocation = new my.Location(LocationName, Description, Country);
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
    }
    
}
