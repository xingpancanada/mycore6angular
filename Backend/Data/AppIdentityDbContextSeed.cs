using Backend.Entities;
using Microsoft.AspNetCore.Identity;

namespace Backend.Data
{
    ///////167. Seeding identity data
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
             if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Xing",
                    Email = "xing@test.com",
                    UserName = "xing@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        Street = "10 The street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    }
                };

                await userManager.CreateAsync(user, "123456Xing$");
            }
        }
    }
}