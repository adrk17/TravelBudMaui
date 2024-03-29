﻿using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using MauiApp1.Services;
using System.Diagnostics;
using my = Resources.Classes;

namespace MauiApp1.ViewModel
{
    public partial class LocationViewModel : BaseViewModel
    {
        public ObservableCollection<my.Location> Locations { get; set; } = new();

        LocationService locationService;


        public LocationViewModel(LocationService locationService)
        {
            Title = "TravelBud";
            this.locationService = locationService;
            
        }

        [RelayCommand]
        async Task GetLocationsAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var locationsFromService = await locationService.GetLocations();

                if (Locations.Count != 0)
                    Locations.Clear();

                foreach (var locationFromService in locationsFromService)
                    Locations.Add(locationFromService);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get locations: {ex.Message}", "OK");

            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToLocationDetailsAsync(my.Location location)
        {
            if (location is null)
                return;

            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await Shell.Current.GoToAsync($"{nameof(LocationDetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {"Location", location}
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to go to this location: {ex.Message}", "OK");

            }
            finally
            {
                IsBusy = false;
            }

        }

        [RelayCommand]
        async Task DeleteLocationAsync(my.Location location)
        {
            bool answer = await Shell.Current.DisplayAlert("Delete "+ "\"" + location.Title + "\"?", "Are you sure?", "Delete", "Cancel");
            if (!answer)
            {
                return;
            }

            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                locationService.RemoveLocation(location);
                var locationsFromService = await locationService.GetLocations();

                if (Locations.Count != 0)
                    Locations.Clear();

                foreach (var locationFromService in locationsFromService)
                    Locations.Add(locationFromService);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get locations: {ex.Message}", "OK");

            }
            finally
            {
                IsBusy = false;
            }
        }


        [RelayCommand]
        async Task GoToAddLocationPageAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddLocationPage)}", true);
        }


        [RelayCommand]
        async Task RefreshLocationsAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var locationsFromService = await locationService.GetLocations(true);

                if (Locations.Count != 0)
                    Locations.Clear();

                foreach (var locationFromService in locationsFromService)
                    Locations.Add(locationFromService);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get locations: {ex.Message}", "OK");

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
