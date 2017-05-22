using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlbertHeijnProductSearcher
{
    public class AlbertHeijnProductSearcher
    {
        private HttpClient _albertHeijnWebsite;
        private const string SHOPURL = "https://www.ah.nl/";

        public string ProductLink { get; private set; }

        public AlbertHeijnProductSearcher()
        {
            _albertHeijnWebsite = new HttpClient();
        }

        public async Task<IEnumerable<ProductInfo>> FindProductSuggestionsAsync(string searchtext)
        {
            var searchText = searchtext.Replace(" ", "+");
            var url = $"{SHOPURL}service/rest/zoeken?rq={searchText}";
            var searchResult = await _albertHeijnWebsite.GetStringAsync(url);
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(searchResult);
            JArray lanes = jsonObject._embedded.lanes;
            var suggestions = lanes.Where(x => x.Value<string>("type") == "SearchLane").FirstOrDefault();
            var suggestions2 = suggestions.SelectToken("_embedded.items").Where(x => x.SelectToken("navItem") != null);
            var suggestions3 = from item in suggestions2
                               select new ProductInfo() { Description = item.SelectToken("_embedded.product.description").ToString(), Url = item.SelectToken("navItem.link.href").ToString() };
            return suggestions3;
        }

        public async Task<ProductInfo> GetProductInfoAsync(ProductInfo productSuggestion)
        {
            var url = $"{SHOPURL}service/rest/delegate?url={productSuggestion.Url}";
            var productInfo = await _albertHeijnWebsite.GetStringAsync(url);
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(productInfo);
            JArray lanes = jsonObject._embedded.lanes;
            var productInfoLanes = lanes.Where(x => x.Value<string>("type") == "StoryLane");
            productSuggestion.Dimensions = productInfoLanes.First().SelectToken("_embedded.items[0]._embedded.sections[0]._embedded.content[3].text.body").Value<string>();
            var nutritionAndIngredientsProductInfoLane = productInfoLanes.Skip(1).First();
            var ingredientsInfo = nutritionAndIngredientsProductInfoLane.SelectToken("_embedded.items[0]._embedded.sections[0]._embedded.content[1].text.body").Value<string>();
            var nutritionInfo = nutritionAndIngredientsProductInfoLane.SelectToken("_embedded.items[1]._embedded.sections[0]._embedded.content[2].text.body").Value<string>();
            var nutritionOverview = new NutritionDataParser().ParseNutritionData(nutritionInfo);
            productSuggestion.NutritionInformation = nutritionOverview;
            productSuggestion.IngredientsInformation = new IngredientsOverview(ingredientsInfo);
            return productSuggestion;
        }
    }
}
