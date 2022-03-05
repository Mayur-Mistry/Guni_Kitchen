using Guni_Kitchen_WebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guni_Kitchen_WebApp.Data
{
    public class ApplicationDbContext 
                        : IdentityDbContext<MyIdentityUser,MyIdentityRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Guni_Kitchen_WebApp.Models.Category> Category { get; set; }
        public DbSet<Guni_Kitchen_WebApp.Models.Order> Order { get; set; }
        public DbSet<Guni_Kitchen_WebApp.Models.Product> Product { get; set; }
    }
}
