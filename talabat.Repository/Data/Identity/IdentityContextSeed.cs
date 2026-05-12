using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Identity;

namespace talabat.Repository.Data.Identity
{
    public static class IdentityContextSeed
    {
        public static async Task Seeding(UserManager<AppUser> userManager)
        {

            if (userManager.Users.Count() == 0)
            {
                var User = new AppUser() 
                { 
                    DisplayName = "Anas Adel",
                    Email= "anas@gmail.com",
                    UserName = "anas",
                    PhoneNumber = "01554349902",
                };
                await userManager.CreateAsync( User ,"Pa$$w0rd");
            }
        } 
    }
}
