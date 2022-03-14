using Guni_Kitchen_WebApp.Models;
using Guni_Kitchen_WebApp.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        /*public DbSet<Guni_Kitchen_WebApp.Models.MyIdentityUser> MyIdentityUsers { get; set; }*/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var sizeConverter = new ValueConverter<ProductSize, string>(
                v=>v.ToString()
                ,v=>(ProductSize)Enum.Parse(typeof(ProductSize),v));
          
            builder.Entity<Category>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("getdate()");

            builder.Entity<Product>()
                .Property(e => e.Product_Price)
                .HasPrecision(precision: 6, scale: 2);

            builder.Entity<Product>()
                    .Property(e => e.Size)
                    .HasConversion(sizeConverter);



        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
