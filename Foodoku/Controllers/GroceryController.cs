using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodoku.Data;
using Foodoku.Models;
using Foodoku.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foodoku.Controllers
{
    public class GroceryController : Controller
    {
        // this private field allows controller to access the database
        private readonly FoodokuDbContext context;
        // actually setting value to this private field
        public GroceryController(FoodokuDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            AddGroceryItemViewModel addGroceryItemViewModel = new AddGroceryItemViewModel();
            addGroceryItemViewModel.GroceryList = context.GroceryItems.Where(g => g.IsInPantry == false).ToList();
            addGroceryItemViewModel.PantryList = context.GroceryItems.Include(p => p.Location).Where(p => p.IsInPantry == true).ToList();

            return View(addGroceryItemViewModel);
        }

        [HttpPost]
        public IActionResult AddToGrocery(AddGroceryItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //creating new grocery item for list
                GroceryItem newGroceryItem = new GroceryItem()
                {
                    Name = viewModel.Name,
                    GroceryNote = viewModel.GroceryNote,
                    IsInPantry = false,
                    LocationID = 4
                };

                // adding and updating database with object
                context.GroceryItems.Add(newGroceryItem);
                context.SaveChanges();

                return Redirect("/Grocery/Index");
            }

            AddGroceryItemViewModel newAddViewModel = new AddGroceryItemViewModel();
            newAddViewModel.GroceryList = context.GroceryItems.Where(g => g.IsInPantry == false).ToList();

            return View("Index", newAddViewModel);
        }

        [HttpPost]
        public IActionResult RemoveFromGrocery(AddGroceryItemViewModel VM, int[] groceryIds)
        {
                // used checkboxes, looping through list of cheeseIds
                foreach (int groceryId in groceryIds)
                {
                    // accessing the existing cheese object
                    GroceryItem theItem = context.GroceryItems.Single(c => c.ID == groceryId);
                    // removing each cheese in the list from the database
                    context.GroceryItems.Remove(theItem);
                }

                // saving changes to the database
                context.SaveChanges();

                // redirecting back to the index to show grocery list
                return Redirect("/Grocery");
        }

        public IActionResult EditGroceryItem(int groceryId)
        {
            GroceryItem grocItem = context.GroceryItems.Single(c => c.ID == groceryId);

            EditGroceryItemViewModel vm = new EditGroceryItemViewModel(grocItem);
            vm.GroceryList = context.GroceryItems.Where(g => g.IsInPantry == false).ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditGroceryItem(EditGroceryItemViewModel vm)
        {
            GroceryItem editedGroceryItem = context.GroceryItems.Single(c => c.ID == vm.GroceryId);

            if (ModelState.IsValid)
            {
                editedGroceryItem.Name = vm.Name;
                editedGroceryItem.GroceryNote = vm.GroceryNote;

                context.SaveChanges();
                return Redirect("/Grocery");
            }

            EditGroceryItemViewModel newEditViewModel = new EditGroceryItemViewModel(editedGroceryItem);
            newEditViewModel.GroceryList = context.GroceryItems.Where(g => g.IsInPantry == false).ToList();

            return View(newEditViewModel);
        }
    }
}
