using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources.Classes
{
    public class Place
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubCategory { get; set; }

        public bool IsVisited { get; set; }

        public Place()
        {
            Title = "Default";
            Description = "Default";
            SubCategory = "Other";
            IsVisited = false;
        }
        public Place(string title, string description, string subCategory = "Other", bool isVisited = false)
        {
            Title = title;
            Description = description;
            SubCategory = subCategory;
            IsVisited = isVisited;
        }

    }
}