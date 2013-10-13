using Core.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Core.Tests.Services{

	public class SaltGeneratorTests : MockContainer {

        [Test]
        public void CreateSalt_Returns10CharSalt()
        {
            const string expectedSalt = "a";

            WebMocks.RandomStringGeneratorMock.Setup(o => o.GetString(It.IsAny<int>(), It.IsAny<string>())).Returns(expectedSalt);

            var sut = GetSut();
            var result = sut.CreateSalt();

            Assert.AreEqual(expectedSalt, result);
        }

        private SaltGenerator GetSut()
        {
            return new SaltGenerator(WebMocks.RandomStringGeneratorMock.Object);
        }

	}

}