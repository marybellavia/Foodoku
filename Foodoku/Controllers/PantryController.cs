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
                    GroceryNote = viewModel.GroceryNote,
                    IsInPantry = true,
                    LocationID = newLocation.ID,
                };

                // adding and updating database with object
                context.GroceryItems.Add(newPantryItem);
                context.SaveChanges();

                return Redirect("/Pantry");
            }

            //AddPantryItemViewModel redoPantryItem = new AddPantryItemViewModel(context.Locations.ToList())
            //{
            //    Name = viewModel.Name,
            //    GroceryNote = viewModel.GroceryNote
            //};
            AddPantryItemViewModel newAddViewModel = new AddPantryItemViewModel(context.Locations.ToList());
            newAddViewModel.PantryItems = context.GroceryItems.ToList();

            return View("Index", newAddViewModel);
        }

        [HttpPost]
        public IActionResult RemoveFromPantry(AddPantryItemViewModel VM, int[] pantryIds)
        {
            // used checkboxes, looping through list of cheeseIds
            foreach (int pantryId in pantryIds)
            {
                // accessing the existing cheese object
                GroceryItem theItem = context.GroceryItems.Single(c => c.ID == pantryId);
                // removing each cheese in the list from the database
                context.GroceryItems.Remove(theItem);
                // edit location
                theItem.LocationID = VM.GroceryItemLocationID;
            }

            // saving changes to the database
            context.SaveChanges();

            // redirecting back to the index to show cheese list without cheeses
            return Redirect("/Pantry");

        }

        public IActionResult EditPantryItem(int pantryId)
        {
            GroceryItem grocItem = context.GroceryItems.Single(c => c.ID == pantryId);

            EditPantryItemViewModel vm = new EditPantryItemViewModel(grocItem, context.Locations.ToList());
            vm.PantryItems = context.GroceryItems.ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditPantryItem(EditPantryItemViewModel vm)
        {
            GroceryItem editedPantryItem = context.GroceryItems.Single(c => c.ID == vm.PantryId);

            if (ModelState.IsValid)
            {
                editedPantryItem.Name = vm.Name;
                editedPantryItem.GroceryNote = vm.GroceryNote;
                editedPantryItem.LocationID = vm.GroceryItemLocationID;

                context.SaveChanges();
                return Redirect("/Pantry");
            }

            EditPantryItemViewModel newEditViewModel = new EditPantryItemViewModel(editedPantryItem, context.Locations.ToList());
            newEditViewModel.PantryItems = context.GroceryItems.ToList();

            return View(newEditViewModel);
        }
    }
}
