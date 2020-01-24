using System;
using System.Collections.Generic;
using Foodoku.Models;

namespace Foodoku.ViewModels
{
    public class EditPantryItemViewModel : AddPantryItemViewModel
    {
        public int PantryId { get; set; }

        public EditPantryItemViewModel() { }

        public EditPantryItemViewModel(GroceryItem item, IEnumerable<GroceryItemLocation> locations) : base(locations)
        {
            // Use Cheese object to initialize the ViewModel properties
            PantryId = item.ID;
            Name = item.Name;
            GroceryNote = item.GroceryNote;
            GroceryItemLocationID = item.LocationID;
        }
    }
}
