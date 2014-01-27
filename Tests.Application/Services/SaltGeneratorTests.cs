using App.Services;
using App.Services.Interfaces;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.App.Services{

	public class SaltGeneratorTests : MockContainer {

        [Test]
        public void CreateSalt_Returns10CharSalt()
        {
            const string expectedSalt = "a";

            GetMock<IRandomStringGenerator>().Setup(o => o.GetString(It.IsAny<int>(), It.IsAny<string>())).Returns(expectedSalt);

            var sut = GetSut();
            var result = sut.CreateSalt();

            Assert.AreEqual(expectedSalt, result);
        }

        private SaltGenerator GetSut()
        {
            return new SaltGenerator(GetMock<IRandomStringGenerator>().Object);
        }

	}

}