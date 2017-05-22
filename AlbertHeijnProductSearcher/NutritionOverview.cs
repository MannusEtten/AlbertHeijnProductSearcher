using System;
using System.Xml.Linq;

namespace AlbertHeijnProductSearcher
{
    public class NutritionOverview
    {
        [NutritionInfo("Inhoud")]
        public string Size { get; set; }
        [NutritionInfo("Energie")]
        public double Energy { get; set; }
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