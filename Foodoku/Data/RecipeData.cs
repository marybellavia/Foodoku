using System;
using System.Collections.Generic;
using System.Linq;
using Foodoku.Models;

namespace Foodoku.Data
{
    class RecipeData
    {
        /**
         * A data store for Recipe objects
         */

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public RecipeFieldData<Title> Titles { get; set; } = new RecipeFieldData<Title>();
        public RecipeFieldData<Summary> Summaries { get; set; } = new RecipeFieldData<Summary>();
        public RecipeFieldData<Yield> Yields { get; set; } = new RecipeFieldData<Yield>();
        public RecipeFieldData<Ingredient> Ingredients { get; set; } = new RecipeFieldData<Ingredient>();
        public RecipeFieldData<Instruction> Instructions { get; set; } = new RecipeFieldData<Instruction>();


        private RecipeData()
        {
            RecipeDataImporter.LoadData(this);
        }

        private static RecipeData instance;
        public static RecipeData GetInstance()
        {
            if (instance == null)
            {
                instance = new RecipeData();
            }

            return instance;
        }


        /**
         * Return all recipe objects in the data store
         * with a field containing the given term
         */
        public List<Recipe> FindByValue(string value)
        {
            var results = from r in Recipes
                          where r.Title.Contains(value)
                          || r.Name.ToLower().Contains(value.ToLower())
                          || r.Ingredient.Contains(value)
                          select r;

            return results.ToList();
        }


        /**
         * Returns results of search the recipes data by key/value, using
         * inclusion of the search term.
         */
        public List<Recipe> FindByColumnAndValue(RecipeFieldType column, string value)
        {
            var results = from r in Recipes
                          where GetFieldByType(r, column).Contains(value)
                          select r;

            return results.ToList();
        }

        /**
         * Returns the RecipeField of the given type from the Recipe object,
         * for all types other than RecipeFieldType.All. In this case, 
         * null is returned.
         */
        public static RecipeField GetFieldByType(Recipe recipe, RecipeFieldType type)
        {
            switch (type)
            {
                case RecipeFieldType.Title:
                    return recipe.Title;
                case RecipeFieldType.Ingredient:
                    return recipe.Ingredient;
            }

            throw new ArgumentException("Cannot get field of type: " + type);
        }


        /**
         * Returns the Job with the given ID,
         * if it exists in the store
         */
        public Recipe Find(int id)
        {
            var results = from r in Recipes
                          where r.ID == id
                          select r;

            return results.Single();
        }

    }
}
