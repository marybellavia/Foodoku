using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodoku.Data;
using Foodoku.Models;
using Foodoku.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//test@test.com Test1234!

namespace Foodoku.Controllers
{
    public class PantryController : Controller
    {
        // this private field allows controller to access the database tables
        private readonly FoodokuDbContext context;
        //usermanager and signin stuffff
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // actually setting value to this private field
        public PantryController(FoodokuDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            context = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // seeding the database if it is empty
                context.Database.EnsureCreated();
                var testLocation = context.Locations.FirstOrDefault(b => b.Name == "Dry Storage");
                if (testLocation == null)
                {
                    context.Locations.Add(new GroceryItemLocation { Name = "Dry Storage" });
                    context.Locations.Add(new GroceryItemLocation { Name = "Fridge" });
                    context.Locations.Add(new GroceryItemLocation { Name = "Freezer" });
                    context.Locations.Add(new GroceryItemLocation { Name = "Deep Freeze" });
                }
                context.SaveChanges();

                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                // creating viewmodel for forms and pantry list
                AddPantryItemViewModel viewModel = new AddPantryItemViewModel(context.Locations.ToList());
                viewModel.PantryList = context.GroceryItems.Where(g => g.IsInPantry == true).Where(u => u.User == currentUser).ToList();

                return View(viewModel);
            }

            return Redirect("/UserSignupLogin/Index");

        }

        [HttpPost]
        public async Task<IActionResult> AddToPantry(AddPantryItemViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                // checking if model is valid
                if (ModelState.IsValid)
                {
                    GroceryItemLocation newLocation =
                    context.Locations.Single
                    (c => c.ID == viewModel.GroceryItemLocationID);

                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);

                    //creating new pantry item for list
                    GroceryItem newPantryItem = new GroceryItem()
                    {
                        Name = viewModel.Name,
                        GroceryNote = viewModel.GroceryNote,
                        IsInPantry = true,
                        LocationID = newLocation.ID,
                        User = currentUser
                    };

                    // adding and updating database with object
                    context.GroceryItems.Add(newPantryItem);
                    context.SaveChanges();

                    return Redirect("/Pantry");
                }

                AddPantryItemViewModel newAddViewModel = new AddPantryItemViewModel(context.Locations.ToList());
                newAddViewModel.PantryList = context.GroceryItems.Where(g => g.IsInPantry == true).ToList();

                return View("Index", newAddViewModel);
            }

            return Redirect("/UserSignupLogin/Index");

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
            }

            // saving changes to the database
            context.SaveChanges();

            // redirecting back to the index to show pantry
            return Redirect("/Pantry");
        }

        public IActionResult EditPantryItem(int pantryId)
        {
            if (User.Identity.IsAuthenticated)
            {
                GroceryItem grocItem = context.GroceryItems.Single(c => c.ID == pantryId);

                EditPantryItemViewModel vm = new EditPantryItemViewModel(grocItem, context.Locations.ToList());
                vm.PantryList = context.GroceryItems.Where(g => g.IsInPantry == true).ToList();

                return View(vm);
            }

            return Redirect("/UserSignupLogin/Index");
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
            newEditViewModel.PantryList = context.GroceryItems.Where(g => g.IsInPantry == true).ToList();

            return View(newEditViewModel);
        }
    }
}
