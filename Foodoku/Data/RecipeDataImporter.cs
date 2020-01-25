using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Foodoku.Models;


namespace Foodoku.Data
{
    public class RecipeDataImporter
    {
        private static bool IsDataLoaded = false;

        /**
         * Load and parse data from job_data.csv
         */
        internal static void LoadData(RecipeData recipeData)
        {

            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("Data/recipe.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                }
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            /**
             * Parse each row array into a Recipe object.
             * Assumes CSV column ordering: 
             *      title, summary, yield, ingredients, instructions
             */
            foreach (string[] row in rows)
            {
                Title title = recipeData.Titles.AddUnique(row[0]);
                Summary summary = recipeData.Summaries.AddUnique(row[1]);
                Yield yield = recipeData.Yields.AddUnique(row[2]);
                Ingredient ingredient = recipeData.Ingredients.AddUnique(row[3]);
                Instruction instruction = recipeData.Instructions.AddUnique(row[4]);

                Recipe newRecipe = new Recipe
                {
                    Name = row[0],
                    Title = title,
                    Summary = summary,
                    Yield = yield,
                    Ingredient = ingredient,
                    Instruction = instruction
                };
                recipeData.Recipes.Add(newRecipe);
            }

            IsDataLoaded = true;
        }


        /**
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }
}
