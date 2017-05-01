
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
            var productInfo = _searcher.GetProductInfo(suggestion);
            productInfo.Ingredients.Count().ShouldBeEquivalentTo(0);
            productInfo.NutritionInformation.Should().BeNull();
        }

        [TestMethod]
        public void GetProductInformation_With_Ingredients_And_Nutrition_Info()
        {
            var suggestions = _searcher.FindProductSuggestionsAsync("muesli+aardbei").Result;
            var suggestion = suggestions.First();
            var productInfo = _searcher.GetProductInfo(suggestion);
            productInfo.Ingredients.Count().ShouldBeEquivalentTo(7);
            productInfo.NutritionInformation.Should().NotBeNull();
            productInfo.NutritionInformation.Sugar.ShouldBeEquivalentTo(14);
        }
    }
}