using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlbertHeijnProductSearcher
{
    internal class NutritionDataParser
    {
        // [table][tr][th][/th][th]Per 100 Gram.[/th][/tr][tr][td]Energie[/td][td]1915 kJ (455 kcal)[/td][/tr][tr][td]Vet[/td][td]17 g[/td][/tr][tr][td]Waarvan verzadigd[/td][td]5,5 g[/td][/tr][tr][td]Waarvan enkelvoudig onverzadigd[/td][td]9 g[/td][/tr][tr][td]Waarvan meervoudig onverzadigd[/td][td]2 g[/td][/tr][tr][td]Koolhydraten[/td][td]63 g[/td][/tr][tr][td]Waarvan suikers[/td][td]25 g[/td][/tr][tr][td]Voedingsvezel[/td][td]7,5 g[/td][/tr][tr][td]Eiwitten[/td][td]9 g[/td][/tr][tr][td]Zout[/td][td]0,9 g[/td][/tr][/table]
        public NutritionOverview ParseNutritionData(string html)
        {
            var htmlDoc = new HtmlDocument();
            var newHtml = html.Replace("[", "<");
            newHtml = newHtml.Replace("]", ">");
            htmlDoc.LoadHtml(newHtml);
            var htmlRows = htmlDoc.DocumentNode.Element("table").Elements("tr");
            var result = new NutritionOverview();
            var firstRow = htmlRows.Where(x => x.Element("th") != null).First();
            result.Size = firstRow.InnerText;
            var otherRows = htmlRows.Where(x => x.Element("td") != null).ToList();
            for(int i = 0; i < otherRows.Count; i++)
            {
                var row = otherRows[i];
                var description = row.FirstChild.InnerText;
                var value = row.LastChild.InnerText;
                SetProperty(result, description, value);
            }
            return result;
        }

        private void SetProperty(NutritionOverview result, string key, string value)
        {
            var typeInfo = result.GetType().GetTypeInfo();
            foreach(var property in typeInfo.DeclaredProperties)
            {
                var attribute = property.GetCustomAttribute<NutritionInfoAttribute>();
                if (attribute != null)
                {
                    if(attribute.Name.Equals(key))
                    {
                        var parsedValue = attribute.ParseValue(value);
                        property.SetValue(result, parsedValue);
                    }
                }
            }
        }
    }
}