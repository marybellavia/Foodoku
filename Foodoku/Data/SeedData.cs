﻿using System.Threading.Tasks;
using Foodoku.Authorization;
using Microsoft.AspNetCore.Identity;
using Foodoku.Data;

namespace Foodoku.Data
{
    public static class SeedData
    {
        public static async Task Initialize(FoodokuDbContext context,
                                        UserManager<IdentityUser> userManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string memberId = "";
            string adminId = "";

            string password = "Testing124!";

            if (await roleManager.FindByNameAsync(Constants.AdministratorsRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorsRole));
            }
            if (await roleManager.FindByNameAsync(Constants.MembersRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.MembersRole));
            }

            if (await userManager.FindByNameAsync("member@cheese.com") == null)
            {
                var user = new IdentityUser("member@cheese.com");
                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, Constants.MembersRole);
                }
                memberId = user.Id;
            }

            if (await userManager.FindByNameAsync("admin@cheese.com") == null)
            {
                var user = new IdentityUser("admin@cheese.com");
                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, Constants.AdministratorsRole);
                }
                adminId = user.Id;
            }
        }



    }
}
