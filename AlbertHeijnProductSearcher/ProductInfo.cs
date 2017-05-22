using Newtonsoft.Json;
using System.Collections.Generic;
using WinUX;

namespace AlbertHeijnProductSearcher
{
    public class ProductInfo
    {
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value.RemoveInvisibleCharacters(); }
        }
        public IEnumerable<dynamic> images { get; set; }
        public IngredientsOverview IngredientsInformation { get; set; }
        public NutritionOverview NutritionInformation { get; set; }
        public string Url { get; internal set; }
        public string Dimensions { get; internal set; }
    }
}