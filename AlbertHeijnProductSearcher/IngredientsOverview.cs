using System.Collections.Generic;
using System.Text;

namespace AlbertHeijnProductSearcher
{
    public class IngredientsOverview
    {
        // "Ingrediënten: 60% volkoren graanvlok (haver, tarwe), suiker, 8% zonnebloemolie, 6%
        // cornflake met yoghurtsmaak [suiker, maïs, gehard palmpitvet, weipoeder, volle melkpoeder,
        // cacaoboter, gemodificeerd tapiocazetmeel, glucosestroop, zout, maltodextrine, emulgator
        // (sojalecithine, E471), natuurlijke aroma's, plantaardige olie (kokos, palm), gerstemout,
        // zuurteregelaar (citroenzuur), glansmiddel (schellak), geleermiddel (arabische gom),
        // conserveermiddel (E200)], geraspte kokos, glucose-fructosestroop, 1,5% gevriesdroogde
        // aardbei, honing, zout, rijsmiddel (E500), karamelstroop, antioxidant (E306).
        // Allergie-informatie: bevat havergluten, tarwegluten, lactose, melkeiwit, soja,
        // gerstgluten. Gemaakt in een bedrijf waar ook pinda's en noten worden verwerkt."

        public IngredientsOverview(string ingredientsAsOneString)
        {
            var result = new List<Ingredient>();
            if (!string.IsNullOrEmpty(ingredientsAsOneString))
            {
                var splitOne = ingredientsAsOneString.Split(":".ToCharArray());
                var splitTwo = splitOne[1].Split(".".ToCharArray());
                var newList = splitTwo[0];
                string newListOfIngredients = ReplaceCommas(newList);
                var parser = new IngredientsParser();
                var ingredients = parser.GetIngredients(newListOfIngredients);
                ingredients.ForEach(x => result.Add(new Ingredient(x.Replace("_", ","))));
            }
            Ingredients = result;
        }

        public IEnumerable<Ingredient> Ingredients { get; private set; }

        private string ReplaceCommas(string listWithIngredients)
        {
            bool replaceComma = false;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < listWithIngredients.Length; i++)
            {
                var character = listWithIngredients[i];
                if (character.Equals('['))
                {
                    replaceComma = true;
                }
                if (character.Equals(']'))
                {
                    replaceComma = false;
                }
                if (replaceComma && character.Equals(','))
                {
                    builder.Append("_");
                }
                else
                {
                    builder.Append(character);
                }
            }
            return builder.ToString();
        }
    }
}