using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.AddBunchTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void CreatesBunch()
        {
            Assert.AreEqual(Id, Added.Id);
            Assert.AreEqual(DisplayName, Added.DisplayName);
            Assert.AreEqual(Description, Added.Description);
            Assert.AreEqual("", Added.HouseRules);
            Assert.AreEqual(TestData.TimeZoneLocal.Id, Added.Timezone.Id);
            Assert.AreEqual(200, Added.DefaultBuyin);
            Assert.AreEqual(CurrencySymbol, Added.Currency.Symbol);
            Assert.AreEqual(CurrencyLayout, Added.Currency.Layout);
        }
    }
}