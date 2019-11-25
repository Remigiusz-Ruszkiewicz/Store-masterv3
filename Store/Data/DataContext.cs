using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>().HasOne(x => x.OwnerUser).WithMany().HasForeignKey(x=>x.OwnerUserId).OnDelete(DeleteBehavior.);
        //}
    }
}
