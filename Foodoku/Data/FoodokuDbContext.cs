using System;
using Foodoku.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foodoku.Data
{
    public class FoodokuDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<GroceryItem> GroceryItems { get; set; }
        public DbSet<GroceryItemLocation> Locations { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // for join table
        //    modelBuilder.Entity<RecipeIngredient>()
        //        .HasKey(c => new { c.RecipeID, c.IngredientID });

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=Foodoku.db");

    }
}
