﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace customOrder.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductType> Products { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<OilOption> OilOptions { get; set; }
        public DbSet<BottleDesign> BottleDesigns { get; set; }
        public DbSet<LogoDesign> LogoDesigns { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductName> ProductNames { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<ShapeWithDesign> ShapeWithDesigns { get; set; }

        public DbSet<Translation> Translations { get; set; }
        public DbSet<TranslationItem> TranslationItems { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Translation>()
                .HasMany(t => t.Items)
                .WithOne(i => i.Translation)
                .HasForeignKey(i => i.TranslationId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        
    }
}






///Add-Migration InitialCreate
///sUpdate - Database

