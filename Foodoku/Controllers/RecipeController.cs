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


        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewAllRecipeViewModel viewModel = new ViewAllRecipeViewModel();

            return View(viewModel);
        }
    }
}