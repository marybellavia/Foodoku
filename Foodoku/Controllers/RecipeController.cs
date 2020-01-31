using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodoku.Data;
using Foodoku.Models;
using Foodoku.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity.UI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foodoku.Controllers
{
    public class RecipeController : Controller
    {
        // this private field allows controller to access the database
        private readonly FoodokuDbContext context;
        private readonly IServices _services;
        // actually setting value to this private field
        public RecipeController(FoodokuDbContext dbContext)
        {

            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewAllRecipeViewModel VM = new ViewAllRecipeViewModel();
            VM.Recipes = context.Recipes.ToList();

            return View(VM);
        }

        public IActionResult ViewRecipe(int recipeId)
        {
            ViewRecipeViewModel VM = new ViewRecipeViewModel();
            Recipe currentRecipe = context.Recipes.Single(r => r.ID == recipeId);

            string[] ingredientsArray = currentRecipe.Ingredients.Split('$', 100);
            string[] instructionsArray = currentRecipe.Instructions.Split('$', 100);

            VM.Recipe = currentRecipe;
            VM.IngredientsArray = ingredientsArray;
            VM.InstructionsArray = instructionsArray;

            return View(VM);
        }
    }
}