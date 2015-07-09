using Core.Entities;
using Core.Exceptions;
using Core.UseCases.AddBunch;
using NUnit.Framework;
using Tests.Common;

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
        public void SetUp()
        {
            _timeZone = Constants.LocalTimeZoneName;
        }

        [Test]
        public void AddBunch_ReturnUrlIsSetToConfirmationUrl()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual("/-/homegame/created", result.ReturnUrl.Relative);
        }

        [Test]
        public void AddBunch_WithEmptyDisplayName_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => Sut.Execute(CreateRequest("")));
        }

        [Test]
        public void AddBunch_WithEmptyCurrencySymbol_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => Sut.Execute(CreateRequest(currencySymbol: "")));
        }

        [Test]
        public void AddBunch_WithEmptyCurrencyLayout_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => Sut.Execute(CreateRequest(currencyLayout: "")));
        }

        [Test]
        public void AddBunch_WithEmptyTimeZone_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() => Sut.Execute(CreateRequest(timeZone: "")));
        }

        [Test]
        public void AddBunch_WithExistingSlug_ThrowsException()
        {
            Assert.Throws<BunchExistsException>(() => Sut.Execute(CreateRequest(ExistingDisplayName)));
        }

        [Test]
        public void AddBunch_WithGoodInput_CreatesBunch()
        {
            Sut.Execute(CreateRequest());

            Assert.AreEqual(0, Repos.Bunch.Added.Id);
            Assert.AreEqual("a-display-name", Repos.Bunch.Added.Slug);
            Assert.AreEqual(DisplayName, Repos.Bunch.Added.DisplayName);
            Assert.AreEqual(Description, Repos.Bunch.Added.Description);
            Assert.AreEqual("", Repos.Bunch.Added.HouseRules);
            Assert.AreEqual(Constants.LocalTimeZone.Id, Repos.Bunch.Added.Timezone.Id);
            Assert.AreEqual(200, Repos.Bunch.Added.DefaultBuyin);
            Assert.AreEqual(CurrencySymbol, Repos.Bunch.Added.Currency.Symbol);
            Assert.AreEqual(CurrencyLayout, Repos.Bunch.Added.Currency.Layout);
        }

        [Test]
        public void AddBunch_WithGoodInput_CreatesPlayer()
        {
            Sut.Execute(CreateRequest());

            Assert.AreEqual(1, Repos.Player.Added.BunchId);
            Assert.AreEqual(3, Repos.Player.Added.UserId);
            Assert.AreEqual(Role.Manager, Repos.Player.Added.Role);
        }

        private AddBunchRequest CreateRequest(string displayName = DisplayName, string currencySymbol = CurrencySymbol, string currencyLayout = CurrencyLayout, string timeZone = null)
        {
            return new AddBunchRequest(Constants.UserNameC, displayName, Description, currencySymbol, currencyLayout, timeZone ?? _timeZone);
        }

        private AddBunchInteractor Sut
        {
            get
            {
                return new AddBunchInteractor(
                    Repos.User,
                    Repos.Bunch,
                    Repos.Player);
            }
        }
    }
}
