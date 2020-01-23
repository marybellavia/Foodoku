using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodoku.Data;
using Foodoku.Models;
using Foodoku.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//test@test.com Test1234!

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
                ViewBag.title0 = "My Pantry";
                ViewBag.title1 = "Current Pantry";
                ViewBag.title2 = "Add Item to Pantry";

                AddPantryItemViewModel viewModel = new AddPantryItemViewModel(context.Locations.ToList());
                viewModel.PantryItems = context.GroceryItems.ToList();

                return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToPantry(AddPantryItemViewModel viewModel)
        {
            // checking if model is valid
            if (ModelState.IsValid)
            {
                GroceryItemLocation newLocation =
                context.Locations.Single
                (c => c.ID == viewModel.GroceryItemLocationID);

                //creating new pantry item for list
                GroceryItem newPantryItem = new GroceryItem()
                {
                    Name = viewModel.Name,
                    IsInPantry = true,
                    LocationID = newLocation.ID,
                };

                // adding and updating database with object
                context.GroceryItems.Add(newPantryItem);
                context.SaveChanges();

                return Redirect("/Pantry");
            }

            return View("Index",viewModel);
        }

        [HttpPost]
        public IActionResult RemoveFromPantry(int[] pantryIds)
        {
            // used checkboxes, looping through list of cheeseIds
            foreach (int pantryId in pantryIds)
            {
                // accessing the existing cheese object
                GroceryItem theItem = context.GroceryItems.Single(c => c.ID == pantryId);
                // removing each cheese in the list from the database
                context.GroceryItems.Remove(theItem);
            }

            // saving changes to the database
            context.SaveChanges();

            // redirecting back to the index to show cheese list without cheeses
            return Redirect("/Pantry");

        }
    }
}
