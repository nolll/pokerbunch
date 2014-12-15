﻿using Core.Exceptions;
using Core.UseCases.AddBunch;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.Builders;

namespace Tests.Core.UseCases
{
    class AddBunchTests : TestBase
    {
        private const string DisplayName = "A Display Name";
        private const string Description = "b";
        private const string CurrencySymbol = "c";
        private const string CurrencyLayout = "d";
        private const string ExistingDisplayName = Constants.BunchNameA;

        private string _timeZone;

        [SetUp]
        public virtual void SetUp()
        {
            _timeZone = TestService.LocalTimeZoneName;
        }

        [Test]
        public void AddBunch_ReturnUrlIsSetToConfirmationUrl()
        {
            Services.Auth.CurrentUser = A.User.Build();

            var result = Execute(CreateRequest());

            Assert.AreEqual("/-/homegame/created", result.ReturnUrl.Relative);
        }

        [Test]
        public void AddBunch_WithEmptyDisplayName_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => Execute(CreateRequest(displayName: "")));
        }

        [Test]
        public void AddBunch_WithEmptyCurrencySymbol_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => Execute(CreateRequest(currencySymbol: "")));
        }

        [Test]
        public void AddBunch_WithEmptyCurrencyLayout_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => Execute(CreateRequest(currencyLayout: "")));
        }

        [Test]
        public void AddBunch_WithEmptyTimeZone_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => Execute(CreateRequest(timeZone: "")));
        }

        [Test]
        public void AddBunch_WithExistingSlug_ThrowsException()
        {
            Assert.Throws<BunchExistsException>(() => Execute(CreateRequest(ExistingDisplayName)));
        }

        private AddBunchRequest CreateRequest(string displayName = DisplayName, string currencySymbol = CurrencySymbol, string currencyLayout = CurrencyLayout, string timeZone = null)
        {
            return new AddBunchRequest(displayName, Description, currencySymbol, currencyLayout, timeZone ?? _timeZone);
        }

        private AddBunchResult Execute(AddBunchRequest request)
        {
            return AddBunchInteractor.Execute(
                Services.Auth,
                Repos.Bunch,
                Repos.Player,
                request);
        }
    }
}
