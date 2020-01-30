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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// for join table
            //modelBuilder.Entity<RecipeIngredient>()
            //    .HasKey(c => new { c.RecipeID, c.IngredientID });
            base.OnModelCreating(modelBuilder);
        }

        public FoodokuDbContext(DbContextOptions<FoodokuDbContext> options)
            : base(options)
        { }
    }
}
