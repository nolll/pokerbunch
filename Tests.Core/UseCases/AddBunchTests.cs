using Core;
using Core.Entities;
using Core.Exceptions;
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
            var result = Execute(CreateRequest());

            Assert.AreEqual("/-/homegame/created", result.ReturnUrl.Relative);
        }

        [Test]
        public void AddBunch_WithEmptyDisplayName_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => Execute(CreateRequest("")));
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

        [Test]
        public void AddBunch_WithGoodInput_CreatesBunch()
        {
            Execute(CreateRequest());

            Assert.AreEqual(0, Repos.Bunch.Added.Id);
            Assert.AreEqual("a-display-name", Repos.Bunch.Added.Slug);
            Assert.AreEqual(DisplayName, Repos.Bunch.Added.DisplayName);
            Assert.AreEqual(Description, Repos.Bunch.Added.Description);
            Assert.AreEqual("", Repos.Bunch.Added.HouseRules);
            Assert.AreEqual(TestService.LocalTimeZone.Id, Repos.Bunch.Added.Timezone.Id);
            Assert.AreEqual(200, Repos.Bunch.Added.DefaultBuyin);
            Assert.AreEqual(CurrencySymbol, Repos.Bunch.Added.Currency.Symbol);
            Assert.AreEqual(CurrencyLayout, Repos.Bunch.Added.Currency.Layout);
        }

        [Test]
        public void AddBunch_WithGoodInput_CreatesPlayer()
        {
            Services.Auth.CurrentIdentity = new CustomIdentity(new UserIdentity{UserId = 3});

            Execute(CreateRequest());

            Assert.AreEqual(1, Repos.Player.Added.BunchId);
            Assert.AreEqual(3, Repos.Player.Added.UserId);
            Assert.AreEqual(Role.Manager, Repos.Player.Added.Role);
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
