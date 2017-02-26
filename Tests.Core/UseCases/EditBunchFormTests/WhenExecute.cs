using System.Linq;
using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.EditBunchFormTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void EditBunchForm_HeadingIsSet()
        {
            Assert.AreEqual("bunch-name Settings", Result.Heading);
        }

        [Test]
        public void EditBunchForm_SlugIsSet()
        {
            Assert.AreEqual(BunchId, Result.Slug);
        }

        [Test]
        public void EditBunchForm_DescriptionIsSet()
        {
            Assert.AreEqual(Description, Result.Description);
        }

        [Test]
        public void EditBunchForm_HouseRulesIsSet()
        {
            Assert.AreEqual(HouseRules, Result.HouseRules);
        }

        [Test]
        public void EditBunchForm_DefaultBuyinIsSet()
        {
            Assert.AreEqual(DefaultBuyin, Result.DefaultBuyin);
        }

        [Test]
        public void EditBunchForm_TimeZoneIdIsSet()
        {
            Assert.AreEqual("UTC", Result.TimeZoneId);
        }

        [Test]
        public void EditBunchForm_CurrencySymbolIsSet()
        {
            Assert.AreEqual(Currency.Default.Symbol, Result.CurrencySymbol);
        }

        [Test]
        public void EditBunchForm_CurrencyLayoutIsSet()
        {
            Assert.AreEqual(Currency.Default.Layout, Result.CurrencyLayout);
        }

        [Test]
        public void EditBunchForm_TimeZonesContainsAllTimezones()
        {
            Assert.AreEqual("Dateline Standard Time", Result.TimeZones.First().Id);
            Assert.AreEqual("Line Islands Standard Time", Result.TimeZones.Last().Id);
        }

        [Test]
        public void EditBunchForm_CurrencyLayoutsAreSet()
        {
            Assert.AreEqual(4, Result.CurrencyLayouts.Count);
            Assert.AreEqual("{SYMBOL} {AMOUNT}", Result.CurrencyLayouts[0]);
            Assert.AreEqual("{SYMBOL}{AMOUNT}", Result.CurrencyLayouts[1]);
            Assert.AreEqual("{AMOUNT}{SYMBOL}", Result.CurrencyLayouts[2]);
            Assert.AreEqual("{AMOUNT} {SYMBOL}", Result.CurrencyLayouts[3]);
        }
    }
}
