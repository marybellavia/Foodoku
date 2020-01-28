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
        // this private field allows controller to access the database tables
        private readonly FoodokuDbContext context;
        //usermanager and signin stuffff
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // actually setting value to this private field
        public UserSignupLoginController(FoodokuDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            context = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserSignupLoginViewModel VM)
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser
                {
                    UserName = VM.AddUserVM.Username,
                    Email = VM.AddUserVM.Email
                };

                var result = await _userManager.CreateAsync(newUser, VM.AddUserVM.Password);

                if (result.Succeeded)
                {
                    var uzer = await _userManager.FindByNameAsync(VM.AddUserVM.Username);

                    if (uzer != null)
                    {
                        var signInResult = await _signInManager.PasswordSignInAsync(uzer, VM.AddUserVM.Password, false, false);
                        if (signInResult.Succeeded)
                        {
                            return Redirect("/Pantry");
                        }
                    }
                }
            }
            return View("Index", VM);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserSignupLoginViewModel VM)
        {
            var uzer = await _userManager.FindByNameAsync(VM.LoginUserVM.Username);

            if (uzer != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(uzer, VM.LoginUserVM.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return Redirect("/Pantry");
                }
            }

            return View("Index", VM);
        }

        public async Task<IActionResult> LogoutUser()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/UserSignupLogin/Index");
        }


    }

    
}
