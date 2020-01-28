using System;
using System.Collections.Generic;

namespace Foodoku.Models
{
    public class UnitOfMeasurement
    {
        public string Name { get; set; }
        public int ID { get; set; }


        public IList<FoodItem> FoodItems { get; set; } 

        public UnitOfMeasurement()
        {
        }
    }
}
