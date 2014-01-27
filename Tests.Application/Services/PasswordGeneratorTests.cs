using Application.Services;
using Application.Services.Interfaces;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.Services{

	public class PasswordGeneratorTests : MockContainer {

		[Test]
        public void CreatePassword_Returns8CharPassword(){
			const string expectedPassword = "a";

            GetMock<IRandomStringGenerator>().Setup(o => o.GetString(It.IsAny<int>(), It.IsAny<string>())).Returns(expectedPassword);

		    var sut = GetSut();
            var result = sut.CreatePassword();

			Assert.AreEqual(expectedPassword, result);
		}

        private PasswordGenerator GetSut()
        {
            return new PasswordGenerator(GetMock<IRandomStringGenerator>().Object);
        }

	}

}