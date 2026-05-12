using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using talabat.Core.Entites.Identity;

namespace talabat.API.Helpers
{
    public static class UserManagerExtensions
    {
       public static async Task<AppUser?> FindUserWithoutAddressByEmailAsync(this UserManager<AppUser> userManager , string email)
        {
           return await userManager.Users.Include(U => U.Address)
                                          .SingleOrDefaultAsync(U=>U.Email==email);
        }
    }
}
