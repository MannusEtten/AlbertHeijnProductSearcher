using System.Collections.Generic;
using WinUX;

namespace AlbertHeijnProductSearcher
{
    public class Ingredient
    {
        public Ingredient(string name)
        {
            Name = name;
            ParseIngredients();
        }

        public List<Ingredient> Ingredients { get; private set; }
        public string Name { get; private set; }

        private void ParseIngredients()
        {
            var result = new List<Ingredient>();
            var startIndex = Name.IndexOf("[");
            if (startIndex > -1)
            {
                var endIndex = Name.IndexOf("]");
                var subIngredients = Name.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
                Name = Name.Replace(subIngredients, string.Empty);
                Name = Name.Substring(0, Name.Length - 2).Trim();
                IngredientsParser parser = new IngredientsParser();
                var ingredients = parser.GetIngredients(subIngredients);
                ingredients.ForEach(x => result.Add(new Ingredient(x.Trim())));
                Ingredients = result;
            }
        }
    }
}