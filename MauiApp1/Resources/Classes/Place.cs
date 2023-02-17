using AndroidX.ConstraintLayout.Core.Motion.Utils;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Classes
{
    public class Place : IComparable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubCategory { get; set; }

        public bool IsVisited { get; set; }
        public bool IsVisible { get; set; }

        public string ImageURL { get; set; }

        public Place()
        {
            Title = "Default";
            Description = "Default";
            SubCategory = "Other";
            IsVisited = false;
            IsVisible = true;
            ImageURL = "";
        }
        public Place(string title, string description, string subCategory = "Other", bool isVisited = false, bool isVisible = true, string imageURL="")
        {
            Title = title;
            Description = description;
            SubCategory = subCategory;
            IsVisited = isVisited;
            IsVisible = isVisible;
            ImageURL = imageURL;
        }

        public int CompareTo(object o)
        {
            Place a = this;
            Place b = (Place)o;
            return b.IsVisible.CompareTo(a.IsVisible);
        }

    }

    static class Extensions
    {
        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            List<T> sorted = collection.OrderBy(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}