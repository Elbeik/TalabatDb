using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domine.Entites.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedIdentityAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Amin Elbeik",
                    Email = "comboell@gmail.com",
                    UserName = "Amin.Asaad",
                    PhoneNumber = "01012649677"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
