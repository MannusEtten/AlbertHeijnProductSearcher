using System.Collections.Generic;

namespace AlbertHeijnProductSearcher
{
    public class IngredientsOverview
    {

        // "Ingrediënten: 60% volkoren graanvlok (haver, tarwe), suiker, 8% zonnebloemolie, 6% cornflake met yoghurtsmaak [suiker, maïs, gehard palmpitvet, weipoeder, volle melkpoeder, cacaoboter, gemodificeerd tapiocazetmeel, glucosestroop, zout, maltodextrine, emulgator (sojalecithine, E471), natuurlijke aroma's, plantaardige olie (kokos, palm), gerstemout, zuurteregelaar (citroenzuur), glansmiddel (schellak), geleermiddel (arabische gom), conserveermiddel (E200)], geraspte kokos, glucose-fructosestroop, 1,5% gevriesdroogde aardbei, honing, zout, rijsmiddel (E500), karamelstroop, antioxidant (E306). Allergie-informatie: bevat havergluten, tarwegluten, lactose, melkeiwit, soja, gerstgluten. Gemaakt in een bedrijf waar ook pinda's en noten worden verwerkt."

        public IngredientsOverview(string ingredientsAsOneString)
        {
            var splitOne = ingredientsAsOneString.Split(":".ToCharArray());
            var splitTwo = splitOne[1].Split(".".ToCharArray());
            var newList = splitTwo[0];
            bool isSummary = false;
            int previousIndex = 0;
            var result = new List<string>();
            for(int i = 0; i < newList.Length; i++)
            {
                var character = newList[i];
                if(character.Equals('('))
                {
                    isSummary = true;
                }
                if (character.Equals(')'))
                {
                    isSummary = false;
                }
                if (character.Equals(',') && newList[i+1].Equals(' ') && isSummary == false)
                {
                    result.Add(newList.Substring(previousIndex +1, i - previousIndex -1).Trim());
                    previousIndex = i;
                }
            }
            Ingredients = result;
        }

        public IEnumerable<string> Ingredients { get; private set; }
    }
}