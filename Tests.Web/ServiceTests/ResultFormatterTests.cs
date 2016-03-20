using Core.Services;
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

            var result = ResultFormatter.FormatWinnings(winnings);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FormatWinnings_WithZeroWinnings_ReturnsTheNumberAsAString()
        {
            const int winnings = 0;
            const string expectedResult = "0";

            var result = ResultFormatter.FormatWinnings(winnings);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void FormatWinnings_WithPositiveWinnings_ReturnsTheNumberAsAString()
        {
            const int winnings = 1;
            const string expectedResult = "+1";

            var result = ResultFormatter.FormatWinnings(winnings);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
