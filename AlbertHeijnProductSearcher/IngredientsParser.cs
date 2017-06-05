using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertHeijnProductSearcher
{
    internal class IngredientsParser
    {
        public List<string> GetIngredients(string listOfIngredients)
        {
            var result = new List<string>();
            int previousIndex = -1;
            bool isSummary = false;
            for (int i = 0; i < listOfIngredients.Length; i++)
            {
                var character = listOfIngredients[i];
                if (character.Equals('('))
                {
                    isSummary = true;
                }
                if (character.Equals(')'))
                {
                    isSummary = false;
                }
                if (character.Equals(',') && listOfIngredients[i + 1].Equals(' ') && isSummary == false)
                {
                    var name = listOfIngredients.Substring(previousIndex + 1, i - previousIndex - 1).Trim();
                    result.Add(name);
                    previousIndex = i;
                }
            }
            return result;
        }
    }
}