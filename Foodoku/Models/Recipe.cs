using System;
using System.Collections.Generic;

namespace Foodoku.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public int ID { get; set; }

        public int Yield { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }

        public IList<RecipeIngredient> Ingredients { get; set; }

        public Recipe()
        {
        }
    }
}
