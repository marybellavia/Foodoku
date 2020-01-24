﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Foodoku.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Foodoku.ViewModels
{
    public class AddPantryItemViewModel
    {

        public IList<GroceryItem> PantryItems { get; set; }

        [Required(ErrorMessage = "Name of item required")]
        public string Name { get; set; }

        public string GroceryNote { get; set; }

        public bool IsInPantry { get; set; }
        
        public int GroceryItemLocationID { get; set; }

        public List<SelectListItem> Locations { get; set; }

        public AddPantryItemViewModel() { }

        public AddPantryItemViewModel(IEnumerable<GroceryItemLocation> locations)
        {
            Locations = new List<SelectListItem>();

            foreach (GroceryItemLocation pantryLoc in locations)
            {
                Locations.Add(new SelectListItem
                {
                    Value = pantryLoc.ID.ToString(),
                    Text = pantryLoc.Name
                });
            }
        }
    }
}
