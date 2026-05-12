using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Identity;

namespace talabat.Core.Services.Contract
{
    public interface IAuthService
    {
        public Task<string> CreateTokenAsync(AppUser user ,UserManager<AppUser> role);
    }
}
