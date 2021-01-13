using System;
using System.Collections.Generic;
using System.Text;

namespace TravelListModels
{
     public class Category
    {
        public int CategoryId { get; set; }
        public string  Name { get; set; }
        public string UserId { get; set; }

        public Category(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
