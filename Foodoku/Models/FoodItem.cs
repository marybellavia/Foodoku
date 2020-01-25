using System;
using System.Collections.Generic;

namespace Foodoku.Models
{
    public abstract class FoodItem
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public FoodItem()
        {
        }
    }

    public class GroceryItem : FoodItem
    {
        public bool IsInPantry { get; set; }
        public string GroceryNote { get; set; }

        public GroceryItemLocation Location { get; set; }
        public int LocationID { get; set; }

        public GroceryItem()
        {
        }
    }

    public class Ingredient : RecipeField
    {
        //public IList<Recipe> Recipes { get; set; }

        public Ingredient()
        {
        }
    }
}
