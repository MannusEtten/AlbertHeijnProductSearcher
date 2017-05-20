using System;
using System.Xml.Linq;

namespace AlbertHeijnProductSearcher
{
    public class NutritionOverview
    {
        // [table][tr][th][/th][th]Per 100 Gram.[/th][/tr][tr][td]Energie[/td][td]1915 kJ (455 kcal)[/td][/tr][tr][td]Vet[/td][td]17 g[/td][/tr][tr][td]Waarvan verzadigd[/td][td]5,5 g[/td][/tr][tr][td]Waarvan enkelvoudig onverzadigd[/td][td]9 g[/td][/tr][tr][td]Waarvan meervoudig onverzadigd[/td][td]2 g[/td][/tr][tr][td]Koolhydraten[/td][td]63 g[/td][/tr][tr][td]Waarvan suikers[/td][td]25 g[/td][/tr][tr][td]Voedingsvezel[/td][td]7,5 g[/td][/tr][tr][td]Eiwitten[/td][td]9 g[/td][/tr][tr][td]Zout[/td][td]0,9 g[/td][/tr][/table]

        public NutritionOverview(string nutritionInHtmlTable)
        {
            var html = StripUnnecessaryHtml(nutritionInHtmlTable);
            var xml = XElement.Parse(nutritionInHtmlTable);

            // query each row
            foreach (var row in xml.Elements("tr"))
            {
                foreach (var item in row.Elements("td"))
                {
                    Console.WriteLine(item.Value);
                }
                Console.WriteLine();
            }
        }

        private string StripUnnecessaryHtml(string nutritionInHtmlTable)
        {
            var result = nutritionInHtmlTable.Replace("[table][tr]", string.Empty);
            result = result.Replace("[tr]", string.Empty);
            result = result.Replace("[/tr]", string.Empty);
            result = result.Replace("[/table]", string.Empty);
            return result;
        }
        [NutritionInfo("Inhoud")]
        public int Size { get; set; }
        [NutritionInfo("Energie")]
        public int Energy { get; set; }
        [NutritionInfo("Vet")]
        public double Fat { get; set; }
        [NutritionInfo("Waarvan verzadigd")]
        public double FatVerzadigd { get; set; }
        [NutritionInfo("Waarvan enkelvoudig onverzadigd")]
        public double SingleOnverzadigd { get; set; }
        [NutritionInfo("Waarvan meervoudig onverzadigd")]
        public double MultipleOnverzadigd { get; set; }
        [NutritionInfo("Koolhydraten")]
        public double Koolhydraten { get; set; }
        [NutritionInfo("Waarvan suikers")]
        public double Sugar { get; set; }
        [NutritionInfo("Voedingsvezel")]
        public double VoedingsVezel { get; set; }
        [NutritionInfo("Eiwitten")]
        public double Eiwitten { get; set; }
        [NutritionInfo("Zout")]
        public double Salt { get; set; }
    }
}