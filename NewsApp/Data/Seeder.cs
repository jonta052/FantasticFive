using NewsApp.Models;
using Microsoft.AspNetCore.Identity;
using NewsApp.Services;

namespace NewsApp.Data
{
    public static class Seeder
    {

        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<User>>();

                await SeedRoles(roleManager);
                await SeedUsers(userManager);
            }
        }
        private static async Task SeedUsers(UserManager<User> userManager)
        {
            var adminUser = new User
            {
                Email = "admin@site.com",
                UserName = "admin@site.com",
                FirstName = "Admin",
                LastName = "User"
            };
            try
            {
                await userManager.CreateAsync(adminUser, "password");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in Roles.RoleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    try
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

        }
    }
}
