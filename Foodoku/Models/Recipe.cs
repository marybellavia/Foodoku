using System;
using System.Collections.Generic;

namespace Foodoku.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public int ID { get; set; }

        public string Summary { get; set; }
        public string Yield { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }

        //public IList<RecipeIngredient> Ingredients { get; set; }

        public Recipe()
        {
        }
    }
}
