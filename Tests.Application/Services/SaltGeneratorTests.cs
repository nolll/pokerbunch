using Application.Services;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.Services
{
	public class SaltGeneratorTests : MockContainer
    {
        [Test]
        public void CreateSalt_Returns10CharSalt()
        {
            var result = SaltGenerator.CreateSalt();

            Assert.AreEqual(10, result.Length);
        }

        [Test]
        public void CreateSalt_GeneratesDifferentSaltsEachTime()
        {
            var result1 = SaltGenerator.CreateSalt();
            var result2 = SaltGenerator.CreateSalt();

            Assert.AreNotEqual(result1, result2);
        }
	}
}