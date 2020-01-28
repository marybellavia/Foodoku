using System;
using System.Collections.Generic;

namespace Foodoku.Models
{
    public class GroceryItemLocation
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<FoodItem> FoodItems { get; set; }

        public GroceryItemLocation()
        {
        }
    }
}
