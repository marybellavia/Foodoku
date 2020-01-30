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
using Foodoku.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//test@test.com Test1234!

namespace Foodoku.Controllers
{
    public class PantryController : Controller
    {
        // this private field allows controller to access the database tables plus authorization stuff
        private FoodokuDbContext Context { get;  }
        private IAuthorizationService AuthorizationService { get;  }
        private UserManager<IdentityUser> UserManager { get; }

        public PantryController(FoodokuDbContext dbContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base()
        {
            Context = dbContext;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            // getting all pantry items
            var pantryItems = from p in Context.Recipes
                              select p;

            var currentUserId = UserManager.GetUserId(User);

            // creating viewmodel for forms and pantry list
            AddPantryItemViewModel viewModel = new AddPantryItemViewModel(Context.Locations.ToList());
            viewModel.PantryList = Context.GroceryItems.Where(g => g.IsInPantry == true).Where(p => p.UserID == currentUserId).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToPantry(AddPantryItemViewModel viewModel)
        {
            // getting user
            var currentUserId = UserManager.GetUserId(User);

            // checking if model is valid
            if (ModelState.IsValid)
            {
                GroceryItemLocation newLocation =
                Context.Locations.Single
                (p => p.ID == viewModel.GroceryItemLocationID);

                //creating new pantry item for list
                GroceryItem newPantryItem = new GroceryItem()
                {
                    Name = viewModel.Name,
                    GroceryNote = viewModel.GroceryNote,
                    IsInPantry = true,
                    LocationID = newLocation.ID,
                    UserID = currentUserId
                };

                //checking if the user has access to create a pantry item
                var isAuthorized = await AuthorizationService.AuthorizeAsync
                    (User, newPantryItem, FoodOperations.Create);

                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                // adding and updating database with object
                Context.GroceryItems.Add(newPantryItem);
                await Context.SaveChangesAsync();

                return Redirect("/Pantry");
            }

            AddPantryItemViewModel newAddViewModel = new AddPantryItemViewModel(Context.Locations.ToList());
            viewModel.PantryList = Context.GroceryItems.Where(g => g.IsInPantry == true).Where(p => p.UserID == currentUserId).ToList();

            return View("Index", newAddViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromPantry(AddPantryItemViewModel VM, int[] pantryIds)
        {
            // used checkboxes, looping through list of cheeseIds
            foreach (int pantryId in pantryIds)
            {
                // accessing the existing cheese object
                GroceryItem theItem = Context.GroceryItems.Single(c => c.ID == pantryId);

                // checking if the user is allowed to delete items from the pantry
                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                    User, theItem,
                                                    FoodOperations.Delete);

                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                // removing each cheese in the list from the database
                Context.GroceryItems.Remove(theItem);
            }

            // saving changes to the database
            await Context.SaveChangesAsync();

            // redirecting back to the index to show pantry
            return Redirect("/Pantry");
        }

        public async Task<IActionResult> EditPantryItem(int pantryId)
        {
            var currentUserId = UserManager.GetUserId(User);

            GroceryItem pantryItem = Context.GroceryItems.Single(c => c.ID == pantryId);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, pantryItem, FoodOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            EditPantryItemViewModel vm = new EditPantryItemViewModel(pantryItem, Context.Locations.ToList());
            vm.PantryList = Context.GroceryItems.Where(g => g.IsInPantry == true).Where(p => p.UserID == currentUserId).ToList();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditPantryItem(EditPantryItemViewModel vm)
        {

            GroceryItem editedPantryItem = Context.GroceryItems.Single(c => c.ID == vm.PantryId);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, editedPantryItem, FoodOperations.Update);

            var currentUserId = UserManager.GetUserId(User);

            if(!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                editedPantryItem.Name = vm.Name;
                editedPantryItem.GroceryNote = vm.GroceryNote;
                editedPantryItem.LocationID = vm.GroceryItemLocationID;

                Context.SaveChanges();
                return Redirect("/Pantry");
            }

            EditPantryItemViewModel newEditViewModel = new EditPantryItemViewModel(editedPantryItem, Context.Locations.ToList());
            newEditViewModel.PantryList = Context.GroceryItems.Where(g => g.IsInPantry == true).Where(p => p.UserID == currentUserId).ToList();

            return View(newEditViewModel);
        }
    }
}
