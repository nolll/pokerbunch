using System.Linq;
using Core.Entities;
using Core.Urls;
using Core.UseCases.EditBunchForm;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EditBunchFormTests : TestBase
    {
        [Test]
        public void EditBunchForm_HeadingIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual("Bunch A Settings", result.Heading);
        }

        [Test]
        public void EditBunchForm_CancelUrlIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<BunchDetailsUrl>(result.CancelUrl);
        }

        [Test]
        public void EditBunchForm_DescriptionIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.DescriptionA, result.Description);
        }

        [Test]
        public void EditBunchForm_HouseRulesIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.HouseRulesA, result.HouseRules);
        }

        [Test]
        public void EditBunchForm_DefaultBuyinIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.DefaultBuyinA, result.DefaultBuyin);
        }

        [Test]
        public void EditBunchForm_TimeZoneIdIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual("UTC", result.TimeZoneId);
        }

        [Test]
        public void EditBunchForm_CurrencySymbolIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Currency.Default.Symbol, result.CurrencySymbol);
        }

        [Test]
        public void EditBunchForm_CurrencyLayoutIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Currency.Default.Layout, result.CurrencyLayout);
        }

        [Test]
        public void EditBunchForm_TimeZonesContainsAllTimezones()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual("Dateline Standard Time", result.TimeZones.First().Id);
            Assert.AreEqual("Line Islands Standard Time", result.TimeZones.Last().Id);
        }

        [Test]
        public void EditBunchForm_CurrencyLayoutsAreSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(4, result.CurrencyLayouts.Count);
            Assert.AreEqual("{SYMBOL} {AMOUNT}", result.CurrencyLayouts[0]);
            Assert.AreEqual("{SYMBOL}{AMOUNT}", result.CurrencyLayouts[1]);
            Assert.AreEqual("{AMOUNT}{SYMBOL}", result.CurrencyLayouts[2]);
            Assert.AreEqual("{AMOUNT} {SYMBOL}", result.CurrencyLayouts[3]);
        }

        private static EditBunchFormRequest CreateRequest()
        {
            return new EditBunchFormRequest(Constants.SlugA);
        }

        private EditBunchFormResult Execute(EditBunchFormRequest request)
        {
            return EditBunchFormInteractor.Execute(
                Repos.Bunch,
                request);
        }
    }
}
