using System.Linq;
using Core.UseCases.AddBunchForm;
using NUnit.Framework;

namespace Tests.Application.UseCases
{
    class AddBunchFormTests
    {
        [Test]
        public void AddBunchForm_TimeZonesContainsAllTimezones()
        {
            var result = Execute();

            Assert.AreEqual("Dateline Standard Time", result.TimeZones.First().Id);
            Assert.AreEqual("Line Islands Standard Time", result.TimeZones.Last().Id);
        }

        [Test]
        public void AddBunchForm_CurrencyLayoutsAreSet()
        {
            var result = Execute();

            Assert.AreEqual(4, result.CurrencyLayouts.Count);
            Assert.AreEqual("{SYMBOL} {AMOUNT}", result.CurrencyLayouts[0]);
            Assert.AreEqual("{SYMBOL}{AMOUNT}", result.CurrencyLayouts[1]);
            Assert.AreEqual("{AMOUNT}{SYMBOL}", result.CurrencyLayouts[2]);
            Assert.AreEqual("{AMOUNT} {SYMBOL}", result.CurrencyLayouts[3]);
        }

        private AddBunchFormResult Execute()
        {
            return AddBunchFormInteractor.Execute();
        }
    }
}
