using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodoku.Authorization;
using Foodoku.Data;
using Foodoku.Models;
using Foodoku.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foodoku.Controllers
{
    public class GroceryController : Controller
    {
        // this private field allows controller to access the database tables plus authorization stuff
        private FoodokuDbContext Context { get; }
        private IAuthorizationService AuthorizationService { get; }
        private UserManager<IdentityUser> UserManager { get; }

        public GroceryController(FoodokuDbContext dbContext,
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
            // getting user
            var currentUserId = UserManager.GetUserId(User);

            AddGroceryItemViewModel addGroceryItemViewModel = new AddGroceryItemViewModel();
            addGroceryItemViewModel.GroceryList = Context.GroceryItems.Where(g => g.IsInPantry == false).Where(g => g.UserID == currentUserId).ToList();
            addGroceryItemViewModel.PantryList = Context.GroceryItems.Include(p => p.Location).Where(p => p.IsInPantry == true).Where(g => g.UserID == currentUserId).ToList();

            return View(addGroceryItemViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToGrocery(AddGroceryItemViewModel viewModel)
        {
            // getting user
            var currentUserId = UserManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                //creating new grocery item for list
                GroceryItem newGroceryItem = new GroceryItem()
                {
                    Name = viewModel.Name,
                    GroceryNote = viewModel.GroceryNote,
                    IsInPantry = false,
                    LocationID = 4,
                    // can't set int to null
                    UserID = currentUserId
                };

                //checking if the user has access to create a pantry item
                var isAuthorized = await AuthorizationService.AuthorizeAsync
                    (User, newGroceryItem, FoodOperations.Create);

                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                // adding and updating database with object
                Context.GroceryItems.Add(newGroceryItem);
                await Context.SaveChangesAsync();

                return Redirect("/Grocery");
            }

            AddGroceryItemViewModel newAddViewModel = new AddGroceryItemViewModel();
            newAddViewModel.GroceryList = Context.GroceryItems.Where(g => g.IsInPantry == false).Where(g => g.UserID == currentUserId).ToList();
            newAddViewModel.PantryList = Context.GroceryItems.Include(p => p.Location).Where(p => p.IsInPantry == true).Where(g => g.UserID == currentUserId).ToList();

            return View("Index", newAddViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromGrocery(int[] groceryIds)
        {
            // used checkboxes, looping through list of cheeseIds
            foreach (int groceryId in groceryIds)
            {
                // accessing the existing cheese object
                GroceryItem theItem = Context.GroceryItems.Single(c => c.ID == groceryId);


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
            Context.SaveChanges();

            // redirecting back to the index to show grocery list
            return Redirect("/Grocery");
        }

        public async Task<IActionResult> EditGroceryItem(int groceryId)
        {
            var currentUserId = UserManager.GetUserId(User);

            GroceryItem grocItem = Context.GroceryItems.Single(g => g.ID == groceryId);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, grocItem, FoodOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            EditGroceryItemViewModel vm = new EditGroceryItemViewModel(grocItem);
            vm.GroceryList = Context.GroceryItems.Where(g => g.IsInPantry == false).Where(g => g.UserID == currentUserId).ToList();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditGroceryItem(EditGroceryItemViewModel vm)
        {
            GroceryItem editedGroceryItem = Context.GroceryItems.Single(c => c.ID == vm.GroceryId);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, editedGroceryItem, FoodOperations.Update);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                editedGroceryItem.Name = vm.Name;
                editedGroceryItem.GroceryNote = vm.GroceryNote;

                Context.SaveChanges();
                return Redirect("/Grocery");
            }

            EditGroceryItemViewModel newEditViewModel = new EditGroceryItemViewModel(editedGroceryItem);
            newEditViewModel.GroceryList = Context.GroceryItems.Where(g => g.IsInPantry == false).Where(g => g.UserID == currentUserId).ToList();

            return View(newEditViewModel);
        }
    }
}
