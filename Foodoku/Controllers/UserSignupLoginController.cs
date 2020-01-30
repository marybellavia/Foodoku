using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodoku.Data;
using Foodoku.Models;
using Foodoku.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foodoku.Controllers
{
    public class UserSignupLoginController : Controller
    {
        // this private field allows controller to access the database
        private readonly FoodokuDbContext context;
        // actually setting value to this private field
        public UserSignupLoginController(FoodokuDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserSignupLoginViewModel VM)
        {
            if (ModelState.IsValid)
            {
                //// creating new user object for database
                //User newUser = VM.AddUserVM.CreateUser();

                //// adding the new user to the database and saving changes
                //context.Users.Add(newUser);
                //context.SaveChanges();

                //// return
                return Redirect("/Pantry");
            }

            return View("Index", VM);
        }

        [HttpPost]
        public IActionResult LoginUser(LoginUserViewModel loginUserViewModel)
        {
            if (ModelState.IsValid)
            {
                return View(loginUserViewModel);
            }

            return View(loginUserViewModel);
        }


    }

    
}
