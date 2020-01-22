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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=Foodoku.db");
    }
}
