using System;
using Foodoku.Models;
using Microsoft.EntityFrameworkCore;

namespace Foodoku.Data
{
    public class FoodokuDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GroceryItem> GroceryItems { get; set; }
        public DbSet<GroceryItemLocation> Locations { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // for join table
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(c => new { c.RecipeID, c.IngredientID });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=Foodoku.db");

    }
}
