using Application.Services;
using NUnit.Framework;

namespace Tests.Web.ServiceTests
{
    public class ResultFormatterTests
    {
        [Test]
        public void FormatWinnings_WithNegativeWinnings_ReturnsTheNumberAsAString()
        {
            const int winnings = -1;
            const string expectedResult = "-1";

            var sut = GetSut();
            var result = sut.FormatWinnings(winnings);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FormatWinnings_WithZeroWinnings_ReturnsTheNumberAsAString()
        {
            const int winnings = 0;
            const string expectedResult = "0";

            var sut = GetSut();
            var result = sut.FormatWinnings(winnings);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FormatWinnings_WithPositiveWinnings_ReturnsTheNumberAsAString()
        {
            const int winnings = 1;
            const string expectedResult = "+1";

            var sut = GetSut();
            var result = sut.FormatWinnings(winnings);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetWinningsCssClass_WithNegativeWinnings_ReturnsNegResult()
        {
            const int winnings = -1;
            const string expectedCssClass = "neg-result";

            var sut = GetSut();
            var result = sut.GetWinningsCssClass(winnings);

            Assert.AreEqual(expectedCssClass, result);
        }

        [Test]
        public void GetWinningsCssClass_WithZeroWinnings_ReturnsEmptyString()
        {
            const int winnings = 0;
            const string expectedCssClass = "";

            var sut = GetSut();
            var result = sut.GetWinningsCssClass(winnings);

            Assert.AreEqual(expectedCssClass, result);
        }

        [Test]
        public void GetWinningsCssClass_WithPositiveWinnings_ReturnsPosResult()
        {
            const int winnings = 1;
            const string expectedCssClass = "pos-result";

            var sut = GetSut();
            var result = sut.GetWinningsCssClass(winnings);

            Assert.AreEqual(expectedCssClass, result);
        }

        private ResultFormatter GetSut()
        {
            return new ResultFormatter();
        }
    }
}
