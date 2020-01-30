using System;
using Foodoku.Models;

namespace Foodoku.ViewModels
{
    public class ViewRecipeViewModel
    {
        public Recipe Recipe { get; set; }

        public string[] IngredientsArray { get; set; }
        public string[] InstructionsArray { get; set; }

        //public static string[] IngredientToArray(string ingredients)
        //{
        //    string[] ingredientsArray = ingredients.Split('$', 50);
        //    return ingredientsArray;
        //}

        //public static string[] InstructionToArray(string instructions)
        //{
        //    string[] instructionsArray = instructions.Split(". ", 50);
        //    return instructionsArray;
        //}


        public ViewRecipeViewModel()
        {
        }
    }
}
