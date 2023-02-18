
using Newtonsoft.Json;
using my = Resources.Classes;

namespace MauiApp1.Services
{
    public class LocationService
    {
        List<my.Location> locations = new List<my.Location>();

        /*
        public async Task ConvertDebugFileToAppData(string sourceFile, string targetFileName)
        {
            // Read the source file
            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(sourceFile);
            using StreamReader reader = new StreamReader(fileStream);

            string content = await reader.ReadToEndAsync();


            // Write the file content to the app data directory
            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);

            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter streamWriter = new StreamWriter(outputStream);

            await streamWriter.WriteAsync(content);
        }*/
        
        public async Task<List<my.Location>> GetLocations()
        {
            try {
                if (locations.Count == 0)
                {
                    string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "locations.json");

                    if (!System.IO.File.Exists(targetFile))
                    {
                        //await ConvertDebugFileToAppData("locations.json", "locations.json");
                        await Shell.Current.DisplayAlert("Error!", "Unable to find locations.json file", "OK");
                        return new List<my.Location>();
                    }


                    using StreamReader reader = new StreamReader(targetFile);

                    string jsonString = await reader.ReadToEndAsync();

                    locations = JsonConvert.DeserializeObject<List<my.Location>>(jsonString);
                    if (locations != null)
                    {
                        await CheckCoordinates(locations);
                    }
                }
            }
             catch(Exception ex)
             {
                    System.Diagnostics.Debug.WriteLine(ex);
                    await Shell.Current.DisplayAlert("Error!", "Unable to convert locations.json to object", "OK");
             }
          
            
            return locations;
        }

        public void RemoveLocation(my.Location location)
        {
            locations.Remove(location);
            SaveLocations();
        }

        public void AddLocation(my.Location location)
        {
            locations.Add(location);
            SaveLocations();
        }

        public void SaveLocations()
        {
            string jsonString = JsonConvert.SerializeObject(locations);
            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "locations.json");
            File.WriteAllText(targetFile, jsonString);
        }


        
        public async Task CheckCoordinates(List<my.Location> locations)
        {
            try{
                foreach (my.Location location in locations)
                {
                    if (location.Latitude == 0 && location.Longitude == 0)
                    {
                        string address = location.Title + " " + location.Country;
                        IEnumerable<Location> mauiLocations = await Geocoding.Default.GetLocationsAsync(address);

                        Location mauiLocation = mauiLocations?.FirstOrDefault();

                        if (mauiLocation != null)
                        {
                            location.Latitude = mauiLocation.Latitude;
                            location.Longitude = mauiLocation.Longitude;
                        }
                    }
                    if(location.Places != null)
                    {
                        foreach (my.Place place in location.Places)
                        {
                            if (place.Latitude == 0 && place.Longitude == 0)
                            {
                                string placeAddress = place.Title + " " + place.SubCategory + " " + location.Title + " " + location.Country;
                                IEnumerable<Location> mauiPlaces = await Geocoding.Default.GetLocationsAsync(placeAddress);

                                Location mauiPlace = mauiPlaces?.FirstOrDefault();

                                if (mauiPlace != null)
                                {
                                    place.Latitude = mauiPlace.Latitude;
                                    place.Longitude = mauiPlace.Longitude;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", "Unable to check coordinates for locations" + ex.Message, "OK");
            }
        }
       
    }
}
