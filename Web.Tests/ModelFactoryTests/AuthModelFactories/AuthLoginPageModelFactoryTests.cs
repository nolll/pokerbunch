using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.UrlModels;

namespace Web.Tests.ModelFactoryTests.AuthModelFactories{

	public class AuthLoginPageModelFactoryTests : MockContainer
	{
		[Test]
        public void ReturnUrl_NoReturnUrl_IsSetToRoot()
		{
		    var sut = GetSut();
		    var result = sut.Create(null, "anyname");

			Assert.AreEqual("/", result.ReturnUrl);
		}

		[Test]
        public void ReturnUrl_WithReturnUrl_IsSet()
		{
		    var sut = GetSut();
            var result = sut.Create("return-url", "anyname");

			Assert.AreEqual("return-url", result.ReturnUrl);
		}

		[Test]
        public void AddUserUrl_IsSet(){
            var sut = GetSut();
			var result = sut.Create("anyurl", "anyname");

			Assert.IsInstanceOf<UserAddUrlModel>(result.AddUserUrl);
		}

		[Test]
        public void ForgotPasswordUrl_IsSet(){
            var sut = GetSut();
			var result = sut.Create("anyurl", "anyname");

			Assert.IsInstanceOf<ForgotPasswordUrlModel>(result.ForgotPasswordUrl);
		}

		[Test]
        public void LoginName_IsSet(){
            var sut = GetSut();
			var result = sut.Create("anyurl", "login-name");

			Assert.AreEqual(result.LoginName, "login-name");
		}

        private AuthLoginPageModelFactory GetSut()
        {
            return new AuthLoginPageModelFactory(PagePropertiesFactoryMock.Object);
        }

	}

}