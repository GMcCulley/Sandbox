using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Database
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, IConfiguration config)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    Email = "developer@ucube.dev",
                    UserName = "DeveloperUser",
                };
                var result = await userManager.CreateAsync(
                    user,
                    config["DefaultUserPassword"] ??
                        throw new InvalidOperationException("Configuration string 'DefaultUserPassword' not found")
                );
            }
        }
    }
}