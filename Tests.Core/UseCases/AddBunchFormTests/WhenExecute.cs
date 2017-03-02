using System.Linq;
using NUnit.Framework;

namespace Tests.Core.UseCases.AddBunchFormTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void TimeZonesContainsAllTimezones()
        {
            Assert.AreEqual("Dateline Standard Time", Result.TimeZones.First().Id);
            Assert.AreEqual("Line Islands Standard Time", Result.TimeZones.Last().Id);
        }

        [Test]
        public void CurrencyLayoutsAreSet()
        {
            Assert.AreEqual(4, Result.CurrencyLayouts.Count);
            Assert.AreEqual("{SYMBOL} {AMOUNT}", Result.CurrencyLayouts[0]);
            Assert.AreEqual("{SYMBOL}{AMOUNT}", Result.CurrencyLayouts[1]);
            Assert.AreEqual("{AMOUNT}{SYMBOL}", Result.CurrencyLayouts[2]);
            Assert.AreEqual("{AMOUNT} {SYMBOL}", Result.CurrencyLayouts[3]);
        }
    }
}
