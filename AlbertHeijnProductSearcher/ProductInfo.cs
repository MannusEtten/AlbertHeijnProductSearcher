using Newtonsoft.Json;
using System.Collections.Generic;
using WinUX;

namespace AlbertHeijnProductSearcher
{
    public class ProductInfo
    {
        private string _description;
        public string id { get; set; }
        [JsonProperty("description")]
        public string Description
        {
            get { return _description; }
            set { _description = value.RemoveInvisibleCharacters(); }
        }
        public IEnumerable<dynamic> images { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
        public NutritionOverview NutritionInformation { get; set; }
    }
}