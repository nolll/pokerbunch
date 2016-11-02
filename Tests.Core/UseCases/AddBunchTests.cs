using Core.Exceptions;
using Core.UseCases;
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

        private string _timeZone;

        [SetUp]
        public void SetUp()
        {
            _timeZone = TestData.LocalTimeZoneName;
        }
        
        [Test]
        public void AddBunch_WithGoodInput_CreatesBunch()
        {
            Sut.Execute(CreateRequest());

            Assert.AreEqual("a-display-name", Deps.Bunch.Added.Id);
            Assert.AreEqual(DisplayName, Deps.Bunch.Added.DisplayName);
            Assert.AreEqual(Description, Deps.Bunch.Added.Description);
            Assert.AreEqual("", Deps.Bunch.Added.HouseRules);
            Assert.AreEqual(TestData.TimeZoneLocal.Id, Deps.Bunch.Added.Timezone.Id);
            Assert.AreEqual(200, Deps.Bunch.Added.DefaultBuyin);
            Assert.AreEqual(CurrencySymbol, Deps.Bunch.Added.Currency.Symbol);
            Assert.AreEqual(CurrencyLayout, Deps.Bunch.Added.Currency.Layout);
        }

        private AddBunch.Request CreateRequest(string displayName = DisplayName, string currencySymbol = CurrencySymbol, string currencyLayout = CurrencyLayout, string timeZone = null)
        {
            return new AddBunch.Request(TestData.UserNameC, displayName, Description, currencySymbol, currencyLayout, timeZone ?? _timeZone);
        }

        private AddBunch Sut => new AddBunch(Deps.Bunch);
    }
}
