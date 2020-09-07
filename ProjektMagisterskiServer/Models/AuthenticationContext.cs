using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektMagisterskiServer.Models
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
             
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<Image> ApplicationImages { get; set; }
    }
}
