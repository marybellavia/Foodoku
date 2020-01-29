﻿// <auto-generated />
using System;
using Foodoku.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Foodoku.Migrations
{
    [DbContext(typeof(FoodokuDbContext))]
    [Migration("20200128202157_RecipeView")]
    partial class RecipeView
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("Foodoku.Models.FoodItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroceryItemLocationID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("GroceryItemLocationID");

                    b.ToTable("FoodItem");

                    b.HasDiscriminator<string>("Discriminator").HasValue("FoodItem");
                });

            modelBuilder.Entity("Foodoku.Models.GroceryItemLocation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Foodoku.Models.Recipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ingredients")
                        .HasColumnType("TEXT");

                    b.Property<string>("Instructions")
                        .HasColumnType("TEXT");

                    b.Property<string>("Summary")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("Yield")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Foodoku.Models.GroceryItem", b =>
                {
                    b.HasBaseType("Foodoku.Models.FoodItem");

                    b.Property<string>("GroceryNote")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsInPantry")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("LocationID");

                    b.HasDiscriminator().HasValue("GroceryItem");
                });

            modelBuilder.Entity("Foodoku.Models.FoodItem", b =>
                {
                    b.HasOne("Foodoku.Models.GroceryItemLocation", null)
                        .WithMany("FoodItems")
                        .HasForeignKey("GroceryItemLocationID");
                });

            modelBuilder.Entity("Foodoku.Models.GroceryItem", b =>
                {
                    b.HasOne("Foodoku.Models.GroceryItemLocation", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
