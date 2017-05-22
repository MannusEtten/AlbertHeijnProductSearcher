
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Linq;
namespace AlbertHeijnProductSearcher.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private AlbertHeijnProductSearcher _searcher;

        [TestInitialize]
        public void Setup()
        {
            _searcher = new AlbertHeijnProductSearcher();
        }

        [TestMethod]
        public void FindProductSuggestions_Get_60_Results()
        {
            var suggestions = _searcher.FindProductSuggestionsAsync("banaan").Result;
            suggestions.Count().ShouldBeEquivalentTo(60);
        }

        [TestMethod]
        public void FindProductSuggestions_Get_Few_Results()
        {
            var suggestions = _searcher.FindProductSuggestionsAsync("muesli+aardbei").Result;
            suggestions.Count().ShouldBeEquivalentTo(3);
        }

        [TestMethod]
        public void GetProductInformation_No_Ingredients_And_No_Nutrition_Info()
        {
            var suggestions = _searcher.FindProductSuggestionsAsync("banaan").Result;
            var suggestion = suggestions.First();
            var productInfo = _searcher.GetProductInfoAsync(suggestion).Result;
            productInfo.Ingredients.Should().NotBeNull();
            productInfo.NutritionInformation.Should().BeNull();
        }

        [TestMethod]
        public void GetProductInformation_With_Ingredients_And_Nutrition_Info()
        {
            var suggestions = _searcher.FindProductSuggestionsAsync("muesli+aardbei").Result;
            var suggestion = suggestions.First();
            var productInfo = _searcher.GetProductInfoAsync(suggestion).Result;
            productInfo.Ingredients.ShouldBeEquivalentTo(7);
            productInfo.NutritionInformation.Should().NotBeNull();
            productInfo.NutritionInformation.Sugar.ShouldBeEquivalentTo(14);
        }

        [TestMethod]
        public void ParseNutritionData()
        {
            var html = "[table][tr][th][/th][th]Per 100 Gram.[/th][/tr][tr][td]Energie[/td][td]1915 kJ (455 kcal)[/td][/tr][tr][td]Vet[/td][td]17 g[/td][/tr][tr][td]Waarvan verzadigd[/td][td]5,5 g[/td][/tr][tr][td]Waarvan enkelvoudig onverzadigd[/td][td]9 g[/td][/tr][tr][td]Waarvan meervoudig onverzadigd[/td][td]2 g[/td][/tr][tr][td]Koolhydraten[/td][td]63 g[/td][/tr][tr][td]Waarvan suikers[/td][td]25 g[/td][/tr][tr][td]Voedingsvezel[/td][td]7,5 g[/td][/tr][tr][td]Eiwitten[/td][td]9 g[/td][/tr][tr][td]Zout[/td][td]0,9 g[/td][/tr][/table]";
            var parser = new NutritionDataParser();
            var parseResult = parser.ParseNutritionData(html);
            parseResult.Sugar.ShouldBeEquivalentTo(25);
            parseResult.Salt.ShouldBeEquivalentTo(0.9);
        }
    }
}