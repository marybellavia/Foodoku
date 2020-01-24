using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Foodoku.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Foodoku.ViewModels
{
    public class AddGroceryItemViewModel
    {
        // list for viewing current pantry in view
        public IList<GroceryItem> PantryItems { get; set; }
        // list for viewing current pantry in view
        public IList<GroceryItem> GroceryList { get; set; }

        // so we can set the name
        [Required(ErrorMessage = "Name of item required")]
        public string Name { get; set; }
        // so we can set note
        public string GroceryNote { get; set; }
        // bool to determine if it is on the grocery list vs. pantry
        public bool IsInPantry { get; set; }

        // to be able to display drop down select list of locations in the add
        public int GroceryItemLocationID { get; set; }
        //public List<SelectListItem> Locations { get; set; }

        public AddGroceryItemViewModel() { }

        //public AddGroceryItemViewModel(IEnumerable<GroceryItemLocation> locations)
        //{
        //    Locations = new List<SelectListItem>();

        //    foreach (GroceryItemLocation pantryLoc in locations)
        //    {
        //        Locations.Add(new SelectListItem
        //        {
        //            Value = pantryLoc.ID.ToString(),
        //            Text = pantryLoc.Name
        //        });
        //    }
        //}
    }
}
