using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Resources.Classes
{
    public class Location
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public ObservableCollection<Place> Places { get; set; }
        public string ImageURL { get; set; }


        public Location()
        {
            Title = "Default";
            Description = "";
            Country = "Unknown";
            Places = new ();
            ImageURL = "";
        }

        public Location(string title, string description, string country, ObservableCollection<Place> places = null, string imageURL = "")
        {
            Title = title;
            Description = description;
            Country = country;
            if (places == null)
                Places = new ();
            else
                Places = places;
            ImageURL = imageURL;
        }

        public void AddPlace(Place place)
        {
            Places.Add(place);
        }

        public void RemovePlace(Place place)
        {
            Places.Remove(place);
        }
        public void RemoveAtPlace(int index)
        {
            Places.RemoveAt(index);
        }
    }


}
