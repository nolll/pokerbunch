using System;
using NUnit.Framework;
using Web.ModelFactories.ChartModelFactories;

namespace Tests.Web.ModelFactoryTests.ChartModelFactories
{
    public class ChartValueModelFactoryTests
    {
        [Test]
        public void Create_WithoutValue_ValueIsEmpty()
        {
            const string expected = "";

            var sut = GetSut();
            var result = sut.Create();

            Assert.AreEqual(expected, result.v);
        }

        [Test]
        public void Create_WithNullValue_ValueIsEmpty()
        {
            var sut = GetSut();
            var result = sut.Create((int?)null);

            Assert.IsNull(result.v);
        }

        [Test]
        public void Create_WithStringValue_ThatValueIsSet()
        {
            const string val = "a";

            var sut = GetSut();
            var result = sut.Create(val);

            Assert.AreEqual(val, result.v);
        }

        [Test]
        public void Create_WithIntValue_ThatValueIsSet()
        {
            const int val = 1;
            const string expected = "1";

            var sut = GetSut();
            var result = sut.Create(val);

            Assert.AreEqual(expected, result.v);
        }

        [Test]
        public void Create_WithDateTimeValue_ThatValueIsFormattedAndSet()
        {
            var val = new DateTime(2001, 2, 3, 4, 5, 6);
            const string expected = "Date(2001, 2, 3, 4, 5, 6)";

            var sut = GetSut();
            var result = sut.Create(val);

            Assert.AreEqual(expected, result.v);
        }

        private ChartValueModelFactory GetSut()
        {
            return new ChartValueModelFactory();
        }
    }
}
