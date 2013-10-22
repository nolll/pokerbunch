using System;
using Core.Classes;
using Infrastructure.System;
using NUnit.Framework;

namespace Infrastructure.Tests.System{

	public class GlobalizationTests {

        [TestCase(0, "0")]
        [TestCase(1, "1")]
        [TestCase(12, "12")]
        [TestCase(123, "123")]
        [TestCase(1234, "1 234")]
        [TestCase(1234567890, "1 234 567 890")]
		public void FormatNumber(int input, string expected)
        {
            var sut = GetSut();
			var result = sut.FormatNumber(input);
			Assert.AreEqual(expected, result);
		}

        [TestCase(1, "1 kr")]
        [TestCase(1234, "1 234 kr")]
		public void FormatCurrency(int input, string expected)
        {
			var currency = new CurrencySettings("kr", "{AMOUNT} {SYMBOL}");
            var sut = GetSut();
			var result = sut.FormatCurrency(currency, input);
			Assert.AreEqual(expected, result);
		}

        [TestCase(1, "1 kr/h")]
        [TestCase(1234, "1 234 kr/h")]
		public void FormatWinrate(int input, string expected)
        {
			var currency = new CurrencySettings("kr", "{AMOUNT} {SYMBOL}");
            var sut = GetSut();
			var result = sut.FormatWinrate(currency, input);
			Assert.AreEqual(expected, result);
		}

        [TestCase(0, "0 kr")]
        [TestCase(1234, "+1 234 kr")]
        [TestCase(-1234, "-1 234 kr")]
		public void FormatResult(int input, string expected)
        {
			var currency = new CurrencySettings("kr", "{AMOUNT} {SYMBOL}");
            var sut = GetSut();
            var result = sut.FormatResult(currency, input);
			Assert.AreEqual(expected, result);
		}

        [TestCase(59, "59m")]
        [TestCase(300, "5h")]
        [TestCase(301, "5h 1m")]
		public void FormatDuration(int input, string expected)
        {
            var sut = GetSut();
			var result = sut.FormatDuration(input);
			Assert.AreEqual(expected, result);
		}

        [TestCase(1, "now")]
        [TestCase(60, "1 minute")]
        [TestCase(120, "2 minutes")]
		public void FormatTimespan(int input, string expected)
        {
            var timespan = TimeSpan.FromSeconds(input);
            var sut = GetSut();
			var result = sut.FormatTimespan(timespan);
			Assert.AreEqual(expected, result);
		}

        [TestCase(false, "Feb 1")]
        [TestCase(true, "Feb 1 2010")]
		public void FormatShortDate(bool includeYear, string expected)
        {
			var dateTime = DateTime.Parse("2010-02-01");
            var sut = GetSut();
            var result = sut.FormatShortDate(dateTime, includeYear);
			Assert.AreEqual(expected, result);
		}

        [TestCase(false, "Feb 1 12:28")]
        [TestCase(true, "Feb 1 2010 12:28")]
		public void FormatShortDateTime(bool includeYear, string expected)
        {
			var dateTime = DateTime.Parse("2010-02-01 12:28:35");
            var sut = GetSut();
            var result = sut.FormatShortDateTime(dateTime, includeYear);
			Assert.AreEqual(expected, result);
		}

        [Test]
        public void FormatTime()
        {
            var dateTime = DateTime.Parse("2010-02-01 12:28:35");
            const string expected = "12:28";
            var sut = GetSut();
            var result = sut.FormatTime(dateTime);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void FormatIsoDate()
        {
			var dateTime = DateTime.Parse("2010-02-01 12:28:35");
            const string expected = "2010-02-01";
            var sut = GetSut();
            var result = sut.FormatIsoDate(dateTime);
			Assert.AreEqual(expected, result);
		}

        [Test]
        public void FormatIsoDateTime()
        {
			var dateTime = DateTime.Parse("2010-02-01 12:28:35");
            const string expected = "2010-02-01 12:28:35";
            var sut = GetSut();
            var result = sut.FormatIsoDateTime(dateTime);
			Assert.AreEqual(expected, result);
		}

        [Test]
		public void FormatYear()
        {
			var dateTime = DateTime.Parse("2010-02-01");
			const string expected = "2010";
            var sut = GetSut();
            var result = sut.FormatYear(dateTime);
            Assert.AreEqual(expected, result);
		}

        private Globalization GetSut()
        {
            return new Globalization();
        }

	}

}