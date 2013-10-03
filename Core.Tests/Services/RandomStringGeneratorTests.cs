using Core.Services;
using NUnit.Framework;

namespace Core.Tests.Services
{
    public class RandomStringGeneratorTests
    {
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        public void GetString_WithSpecifiedLength_ReturnsStringOfCorrectLength(int specifiedLength, int expectedLength)
        {
            var sut = GetSut();
            var result = sut.GetString(specifiedLength, "any");

            Assert.AreEqual(expectedLength, result.Length);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetString_WithNoAllowedCharacters_ReturnsEmptyString(string allowedCharacters)
        {
            const int specifiedLength = 1;

            var sut = GetSut();
            var result = sut.GetString(specifiedLength, allowedCharacters);

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void GetString_WithAllowedCharacters_ReturnsStringThatContainsOnlyAllowedCharacters()
        {
            const int specifiedLength = 3;
            const string allowedCharacters = "a";
            const string expected = "aaa";

            var sut = GetSut();
            var result = sut.GetString(specifiedLength, allowedCharacters);

            Assert.AreEqual(expected, result);
        }

        private RandomStringGenerator GetSut()
        {
            return new RandomStringGenerator();
        }
    }
}
