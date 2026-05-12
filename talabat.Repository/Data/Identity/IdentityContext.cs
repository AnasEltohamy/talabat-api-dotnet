using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Identity;

namespace talabat.Repository.Data.Identity
{
    public class IdentityContext:IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options)
        {
            
        }


        public DbSet<AppUser> appUsers { get; set; }



    }
}
