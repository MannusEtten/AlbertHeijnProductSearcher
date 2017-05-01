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

        public AlbertHeijnProductSearcher()
        {
            _albertHeijnWebsite = new HttpClient();
        }

        public async Task<IEnumerable<ProductInfo>> FindProductSuggestionsAsync(string searchtext)
        {
            var searchText = searchtext.Replace(" ", "+");
            var url = $"https://www.ah.nl/service/rest/zoeken?rq={searchText}";
            var searchResult = await _albertHeijnWebsite.GetStringAsync(url);
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(searchResult);
            JArray lanes = jsonObject._embedded.lanes;
            var suggestions = lanes.Where(x => x.Value<string>("type") == "SearchLane").FirstOrDefault();
            var suggestions2 = suggestions.SelectToken("_embedded.items");
            var suggestions3= from item in suggestions2
                           select item.SelectToken("_embedded.product");
            return suggestions3.Where(x => x != null).Select(x => x.ToObject<ProductInfo>());
        }

        public ProductInfo GetProductInfo(object productSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
