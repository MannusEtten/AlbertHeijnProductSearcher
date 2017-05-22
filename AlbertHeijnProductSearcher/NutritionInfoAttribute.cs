using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertHeijnProductSearcher
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NutritionInfoAttribute : Attribute
    {
        public NutritionInfoAttribute(string name)
        {
            Name = name;
        }
 
        public string Name { get; set; }

        public double ParseValue(string value)
        {
            if(value.Contains("kcal"))
            {
                var firstIndex = value.IndexOf("(") +1;
                var lastIndex = value.IndexOf("kcal");
                var result = value.Substring(firstIndex, lastIndex - firstIndex);
                return int.Parse(result);
            }
            if (value.Contains("g"))
            {
                var firstIndex = 0;
                var lastIndex = value.IndexOf("g");
                var result = value.Substring(firstIndex, lastIndex - firstIndex);
                var culture = new CultureInfo("NL-nl");
                return double.Parse(result, culture);
            }
            return 0;
        }
    }
}