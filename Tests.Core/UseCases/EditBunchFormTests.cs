using System;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.EditBunchForm;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EditBunchFormTests : TestBase
    {
        private const string Slug = "a";
        private const string BunchName = "b";
        private const string Description = "c";
        private const string HouseRules = "d";
        private const int DefaultBuyin = 1;
        private const string TimeZoneId = "UTC";
        private const string CurrencySymbol = "f";
        private const string CurrencyLayout = "g";

        [Test]
        public void EditBunchForm_HeadingIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual("b Settings", result.Heading);
        }

        [Test]
        public void EditBunchForm_CancelUrlIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<BunchDetailsUrl>(result.CancelUrl);
        }

        [Test]
        public void EditBunchForm_DescriptionIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(Description, result.Description);
        }

        [Test]
        public void EditBunchForm_HouseRulesIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(HouseRules, result.HouseRules);
        }

        [Test]
        public void EditBunchForm_DefaultBuyinIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(DefaultBuyin, result.DefaultBuyin);
        }

        [Test]
        public void EditBunchForm_TimeZoneIdIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(TimeZoneId, result.TimeZoneId);
        }

        [Test]
        public void EditBunchForm_CurrencySymbolIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(CurrencySymbol, result.CurrencySymbol);
        }

        [Test]
        public void EditBunchForm_CurrencyLayoutIsSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(CurrencyLayout, result.CurrencyLayout);
        }

        [Test]
        public void EditBunchForm_TimeZonesContainsAllTimezones()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual("Dateline Standard Time", result.TimeZones.First().Id);
            Assert.AreEqual("Line Islands Standard Time", result.TimeZones.Last().Id);
        }

        [Test]
        public void EditBunchForm_CurrencyLayoutsAreSet()
        {
            SetupBunch();

            var result = Execute(CreateRequest());

            Assert.AreEqual(4, result.CurrencyLayouts.Count);
            Assert.AreEqual("{SYMBOL} {AMOUNT}", result.CurrencyLayouts[0]);
            Assert.AreEqual("{SYMBOL}{AMOUNT}", result.CurrencyLayouts[1]);
            Assert.AreEqual("{AMOUNT}{SYMBOL}", result.CurrencyLayouts[2]);
            Assert.AreEqual("{AMOUNT} {SYMBOL}", result.CurrencyLayouts[3]);
        }

        private void SetupBunch()
        {
            var bunch = CreateBunch();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
        }

        private Bunch CreateBunch()
        {
            return A.Bunch
                .WithDisplayName(BunchName)
                .WithDescription(Description)
                .WithHouseRules(HouseRules)
                .WithSlug(Slug)
                .WithTimeZone(TimeZoneInfo.Utc)
                .WithDefaultBuyin(DefaultBuyin)
                .WithCurrency(new Currency(CurrencySymbol, CurrencyLayout))
                .Build();
        }

        private static EditBunchFormRequest CreateRequest()
        {
            return new EditBunchFormRequest(Slug);
        }

        private EditBunchFormResult Execute(EditBunchFormRequest request)
        {
            return EditBunchFormInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                request);
        }
    }
}
