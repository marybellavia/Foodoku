using System.Threading.Tasks;
using Foodoku.Authorization;
using Microsoft.AspNetCore.Identity;
using Foodoku.Data;
using System.Linq;
using Foodoku.Models;

namespace Foodoku.Data
{
    public static class SeedData
    {
        public static async Task Initialize(FoodokuDbContext context,
                                        UserManager<IdentityUser> userManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // seeding pantry locations database
            var testLocation = context.Locations.FirstOrDefault(b => b.Name == "Dry Storage");
            if (testLocation == null)
            {
                context.Locations.Add(new GroceryItemLocation { Name = "Dry Storage" });
                context.Locations.Add(new GroceryItemLocation { Name = "Fridge" });
                context.Locations.Add(new GroceryItemLocation { Name = "Freezer" });
                context.Locations.Add(new GroceryItemLocation { Name = "Deep Freeze" });
            }
            context.SaveChanges();


            // seeding Admin and foodie users
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

            if (await userManager.FindByNameAsync("foodie@foodoku.com") == null)
            {
                var user = new IdentityUser("foodie@foodoku.com");
                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, Constants.MembersRole);
                }
                memberId = user.Id;
            }

            if (await userManager.FindByNameAsync("admin@foodoku.com") == null)
            {
                var user = new IdentityUser("admin@foodoku.com");
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
