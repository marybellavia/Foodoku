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
    public class PantryController : Controller
    {
        // this private field allows controller to access the database
        private readonly FoodokuDbContext context;
        // actually setting value to this private field
        public PantryController(FoodokuDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.title = "My Pantry";

            IList<GroceryItem> pantryItems = context.GroceryItems.ToList();

            return View(pantryItems);
        }

        public IActionResult Add()
        {
            ViewBag.title = "Add item to pantry";
            AddPantryItemViewModel viewModel = new AddPantryItemViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddPantryItemViewModel viewModel)
        {
            ViewBag.title = "Add item to pantry";

            if (ModelState.IsValid)
            {
                GroceryItem newPantryItem = new GroceryItem()
                {
                    Name = viewModel.Name,
                    IsInPantry = true,
                };

                context.GroceryItems.Add(newPantryItem);
                context.SaveChanges();

                IList<GroceryItem> pantryItems = context.GroceryItems.ToList();

                return View("Index", pantryItems);
            }

            return View(viewModel);
        }
    }
}
