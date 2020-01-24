using System;
using Foodoku.Models;

namespace Foodoku.ViewModels
{
    public class EditGroceryItemViewModel : AddGroceryItemViewModel
    {

        public int GroceryId { get; set; }

        public EditGroceryItemViewModel() {}

        public EditGroceryItemViewModel(GroceryItem item) : base()
        {
            // Use Cheese object to initialize the ViewModel properties
            GroceryId = item.ID;
            Name = item.Name;
            GroceryNote = item.GroceryNote;
        }

    }
}
