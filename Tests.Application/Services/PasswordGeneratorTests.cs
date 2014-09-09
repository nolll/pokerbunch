using Application.Services;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.Services
{
    public class PasswordGeneratorTests : TestBase
    {
        [Test]
        public void CreatePassword_Returns8CharPassword()
        {
            var result = PasswordGenerator.CreatePassword();

            Assert.AreEqual(8, result.Length);
        }

        [Test]
        public void CreatePassword_GeneratesDifferentPasswordsEachTime()
        {
            var result1 = PasswordGenerator.CreatePassword();
            var result2 = PasswordGenerator.CreatePassword();

            Assert.AreNotEqual(result1, result2);
        }
    }
}