using Application.UseCases.AddBunchForm;
using NUnit.Framework;

namespace Tests.Application.UseCases
{
    class AddBunchFormTests
    {
        [Test]
        public void AddBunchForm_TimeZonesContainsAllTimezones()
        {
            var result = Execute();

            Assert.AreEqual(103, result.TimeZones.Count);
            Assert.AreEqual("Dateline Standard Time", result.TimeZones[0].Id);
            Assert.AreEqual("Line Islands Standard Time", result.TimeZones[102].Id);
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
