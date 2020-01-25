using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodoku.Data;
using Foodoku.Models;
using Foodoku.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foodoku.Controllers
{
    public class RecipeController : Controller
    {
        // this private field allows controller to access the database
        private readonly FoodokuDbContext context;
        // actually setting value to this private field
        public RecipeController(FoodokuDbContext dbContext)
        {
            context = dbContext;
        }

        private static RecipeData recipeData;

        static RecipeController()
        {
            recipeData = RecipeData.GetInstance();
        }


        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            Recipe recipe = RecipeData.GetInstance().Find(id);

            return View(recipe);
        }
    }
}