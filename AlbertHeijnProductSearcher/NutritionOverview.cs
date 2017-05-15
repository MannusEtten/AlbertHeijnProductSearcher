namespace AlbertHeijnProductSearcher
{
    public class NutritionOverview
    {
        // [table][tr][th][/th][th]Per 100 Gram.[/th][/tr][tr][td]Energie[/td][td]1915 kJ (455 kcal)[/td][/tr][tr][td]Vet[/td][td]17 g[/td][/tr][tr][td]Waarvan verzadigd[/td][td]5,5 g[/td][/tr][tr][td]Waarvan enkelvoudig onverzadigd[/td][td]9 g[/td][/tr][tr][td]Waarvan meervoudig onverzadigd[/td][td]2 g[/td][/tr][tr][td]Koolhydraten[/td][td]63 g[/td][/tr][tr][td]Waarvan suikers[/td][td]25 g[/td][/tr][tr][td]Voedingsvezel[/td][td]7,5 g[/td][/tr][tr][td]Eiwitten[/td][td]9 g[/td][/tr][tr][td]Zout[/td][td]0,9 g[/td][/tr][/table]

        public NutritionOverview(string nutritionInHtmlTable)
        {


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

        public int Energy { get; set; }
        public double Fat { get; set; }
        public double FatVerzadigd { get; set; }
        public double Koolhydraten { get; set; }
        public double Sugar { get; set; }
        public double Eiwitten { get; set; }
        public double Salt { get; set; }
    }
}