using System;
using Foodoku.Models;

namespace Foodoku.ViewModels
{
    public class AddPantryItemViewModel
    {
        public string Name { get; set; }
        public bool IsInPantry { get; set; }

        public AddPantryItemViewModel()
        {
        }
    }
}
