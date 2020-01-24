using System;
namespace Foodoku.Models
{
    public class RecipeIngredient
    {

        public Recipe Recipe { get; set; }
        public int RecipeID { get; set; }

        public Ingredient Ingredient { get; set; }
        public int IngredientID { get; set; }

        public RecipeIngredient()
        {
        }
    }
}