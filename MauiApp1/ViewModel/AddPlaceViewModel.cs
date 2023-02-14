﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using my = Resources.Classes;

namespace MauiApp1.ViewModel
{
    [QueryProperty("Location", "Location")]
    public partial class AddPlaceViewModel : BaseViewModel
    {

        LocationService locationService;
        
        [ObservableProperty]
        my.Location location;
        public AddPlaceViewModel(LocationService locationService)
        {
            this.locationService = locationService;
            this.Title = "Add a new place in ...";
        }

        [ObservableProperty]
        string placeName;

        [ObservableProperty]
        string placeDescription;

        [ObservableProperty]
        string placeSubCategory;


        [RelayCommand]
        async Task AddNewPlaceAsync()
        {
            if (string.IsNullOrWhiteSpace(PlaceName) || string.IsNullOrWhiteSpace(PlaceDescription) || string.IsNullOrWhiteSpace(PlaceSubCategory))
            {
                await Shell.Current.DisplayAlert("Error!", "Please fill out all fields", "OK");
                return;
            }
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                my.Place newPlace = new my.Place(PlaceName, PlaceDescription, PlaceSubCategory);
                Location.Places.Add(newPlace);
                locationService.SaveLocations();

                await Shell.Current.GoToAsync("../../");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to add a place: {ex.Message}", "OK");

            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}