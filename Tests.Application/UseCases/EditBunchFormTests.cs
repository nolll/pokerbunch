using System;
using Application.Urls;
using Application.UseCases.EditBunchForm;
using Core.Entities;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class EditBunchFormTests : MockContainer
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

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual("b Settings", result.Heading);
        }

        [Test]
        public void EditBunchForm_CancelUrlIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.IsInstanceOf<BunchDetailsUrl>(result.CancelUrl);
        }

        [Test]
        public void EditBunchForm_DescriptionIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(Description, result.Description);
        }

        [Test]
        public void EditBunchForm_HouseRulesIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(HouseRules, result.HouseRules);
        }

        [Test]
        public void EditBunchForm_DefaultBuyinIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(DefaultBuyin, result.DefaultBuyin);
        }

        [Test]
        public void EditBunchForm_TimeZoneIdIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TimeZoneId, result.TimeZoneId);
        }

        [Test]
        public void EditBunchForm_CurrencySymbolIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(CurrencySymbol, result.CurrencySymbol);
        }

        [Test]
        public void EditBunchForm_CurrencyLayoutIsSet()
        {
            SetupBunch();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(CurrencyLayout, result.CurrencyLayout);
        }

        private void SetupBunch()
        {
            var bunch = CreateBunch();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
        }

        private static BunchInTest CreateBunch()
        {
            return new BunchInTest(
                displayName: BunchName,
                description: Description,
                houseRules: HouseRules,
                slug: Slug,
                timezone: TimeZoneInfo.Utc,
                defaultBuyin: DefaultBuyin,
                currency: new Currency(CurrencySymbol, CurrencyLayout));
        }

        private static EditBunchFormRequest CreateRequest()
        {
            return new EditBunchFormRequest(Slug);
        }

        private EditBunchFormInteractor Sut
        {
            get
            {
                return new EditBunchFormInteractor(GetMock<IBunchRepository>().Object);
            }
        }
    }
}
