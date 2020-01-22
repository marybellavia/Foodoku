using System;
using System.Collections.Generic;
using Foodoku.Models;

namespace Foodoku.ViewModels
{
    public class AddPantryItemViewModel
    {

        public IList<GroceryItem> PantryItems { get; set; }

        public string Name { get; set; }
        public bool IsInPantry { get; set; }

        public AddPantryItemViewModel()
        {
        }
    }
}
