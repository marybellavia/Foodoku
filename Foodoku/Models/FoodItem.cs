﻿using System;
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

        public GroceryItem()
        {
        }
    }

    //public class RecipeIngredient : FoodItem
    //{
    //    public RecipeIngredient()
    //    {
    //    }
    //}
}
