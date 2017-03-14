using NUnit.Framework;

namespace Tests.Core.UseCases.EditBunchTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void SavesBunch()
        {
            Assert.AreEqual(Description, Saved.Description);
            Assert.AreEqual(CurrencySymbol, Saved.Currency.Symbol);
            Assert.AreEqual(CurrencyLayout, Saved.Currency.Layout);
            Assert.AreEqual(Timezone, Saved.Timezone.Id);
            Assert.AreEqual(HouseRules, Saved.HouseRules);
            Assert.AreEqual(DefaultBuyin, Saved.DefaultBuyin);
        }

        [Test]
        public void BunchIdIsSet()
        {
            Assert.AreEqual(BunchId, Result.BunchId);
        }
    }
}