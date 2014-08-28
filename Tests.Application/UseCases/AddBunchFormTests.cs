using Application.UseCases.AddBunchForm;
using NUnit.Framework;

namespace Tests.Application.UseCases
{
    class AddBunchFormTests
    {
        [Test]
        public void AddBunchForm_TimeZonesContainsAllTimezones()
        {
            var result = Sut.Execute();

            Assert.AreEqual(103, result.TimeZones.Count);
            Assert.AreEqual("Dateline Standard Time", result.TimeZones[0].Id);
            Assert.AreEqual("(UTC-12:00) International Date Line West", result.TimeZones[0].Name);
            Assert.AreEqual("Line Islands Standard Time", result.TimeZones[102].Id);
            Assert.AreEqual("(UTC+14:00) Kiritimati Island", result.TimeZones[102].Name);
        }

        [Test]
        public void AddBunchForm_CurrencyLayoutsAreSet()
        {
            var result = Sut.Execute();

            Assert.AreEqual(4, result.CurrencyLayouts.Count);
            Assert.AreEqual("{SYMBOL} {AMOUNT}", result.CurrencyLayouts[0]);
            Assert.AreEqual("{SYMBOL}{AMOUNT}", result.CurrencyLayouts[1]);
            Assert.AreEqual("{AMOUNT}{SYMBOL}", result.CurrencyLayouts[2]);
            Assert.AreEqual("{AMOUNT} {SYMBOL}", result.CurrencyLayouts[3]);
        }

        private AddBunchFormInteractor Sut
        {
            get { return new AddBunchFormInteractor(); }
        }
    }
}
