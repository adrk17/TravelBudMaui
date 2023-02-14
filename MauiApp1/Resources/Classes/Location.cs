using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Classes
{
    public class Location
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public List<Place> Places { get; set; }

        public Location()
        {
            Title = "Default";
            Description = "Default";
            Country = "Unknown";
            Places = new List<Place>();
        }

        public Location(string title, string description, string country, List<Place> places = null)
        {
            Title = title;
            Description = description;
            Country = country;
            if (places == null)
                Places = new List<Place>();
            else
                Places = places;
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
