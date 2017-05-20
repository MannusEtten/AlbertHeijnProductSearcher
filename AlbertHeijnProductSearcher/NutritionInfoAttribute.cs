using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertHeijnProductSearcher
{
    public class NutritionInfoAttribute : Attribute
    {
        private string _name;
        public NutritionInfoAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}