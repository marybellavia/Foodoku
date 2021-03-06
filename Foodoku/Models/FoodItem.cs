﻿using System;
using System.Collections.Generic;

namespace Foodoku.Models
{
    public abstract class FoodItem : BindingAgent
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

    public class Ingredient : FoodItem
    {
        //public string Quantity { get; set; }

        //public UnitOfMeasurement UnitOfMeasurement { get; set; }
        //public int UnitOfMeasurementID { get; set; }

        //public IList<Recipe> Recipes { get; set; }

        public Ingredient()
        {
        }
    }
}
