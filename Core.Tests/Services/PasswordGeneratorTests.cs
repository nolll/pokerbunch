using Core.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Core.Tests.Services{

	public class PasswordGeneratorTests : MockContainer {

		[Test]
        public void CreatePassword_Returns8CharPassword(){
			const string expectedPassword = "a";

            WebMocks.RandomStringGeneratorMock.Setup(o => o.GetString(It.IsAny<int>(), It.IsAny<string>())).Returns(expectedPassword);

		    var sut = GetSut();
            var result = sut.CreatePassword();

			Assert.AreEqual(expectedPassword, result);
		}

        private PasswordGenerator GetSut()
        {
            return new PasswordGenerator(WebMocks.RandomStringGeneratorMock.Object);
        }

	}

}