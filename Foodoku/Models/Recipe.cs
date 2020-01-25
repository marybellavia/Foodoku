using System;
using System.Collections.Generic;

namespace Foodoku.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        private static int nextId = 1;

        public string Name { get; set; }
        public Title Title { get; set; }
        public Summary Summary { get; set; }
        public Yield Yield { get; set; }
        public Ingredient Ingredient { get; set; }
        public Instruction Instruction { get; set; }

        //public IList<RecipeIngredient> Ingredients { get; set; }

        public Recipe()
        {
            ID = nextId;
            nextId++;
        }
    }
}
