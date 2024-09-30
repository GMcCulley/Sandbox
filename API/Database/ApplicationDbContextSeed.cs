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
                    Email = config["DefaultUser:Email"] ??
                        throw new InvalidOperationException("Configuration string 'DefaultUser:Email' not found"),
                    UserName = config["DefaultUser:Username"] ??
                        throw new InvalidOperationException("Configuration string 'DefaultUser:Username' not found"),
                };
                var result = await userManager.CreateAsync(
                    user,
                    config["DefaultUser:Password"] ??
                        throw new InvalidOperationException("Configuration string 'DefaultUser:Password' not found")
                );
            }
        }
    }
}
