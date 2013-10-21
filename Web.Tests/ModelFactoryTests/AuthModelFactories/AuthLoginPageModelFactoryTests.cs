using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;

namespace Web.Tests.ModelFactoryTests.AuthModelFactories{

	public class AuthLoginPageModelFactoryTests : WebMockContainer
	{
		[Test]
        public void ReturnUrl_NoReturnUrl_IsSetToRoot()
		{
            const string homeUrl = "a";
            Mocks.UrlProviderMock.Setup(o => o.GetHomeUrl()).Returns(homeUrl);

		    var sut = GetSut();

		    var result = sut.Create();

			Assert.AreEqual(homeUrl, result.ReturnUrl);
		}

		[Test]
        public void ReturnUrl_WithReturnUrl_IsSet()
		{
            Mocks.WebContextMock.Setup(o => o.GetQueryParam("return")).Returns("return-url");

		    var sut = GetSut();
            var result = sut.Create();

			Assert.AreEqual("return-url", result.ReturnUrl);
		}

		[Test]
        public void AddUserUrl_IsSet()
		{
		    const string loginUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetAddUserUrl()).Returns(loginUrl);

            var sut = GetSut();
			var result = sut.Create();

			Assert.AreEqual(loginUrl, result.AddUserUrl);
		}

		[Test]
        public void ForgotPasswordUrl_IsSet(){
            const string forgotPasswordUrl = "a";
            Mocks.UrlProviderMock.Setup(o => o.GetForgotPasswordUrl()).Returns(forgotPasswordUrl);

            var sut = GetSut();
			var result = sut.Create();

			Assert.AreEqual(forgotPasswordUrl, result.ForgotPasswordUrl);
		}

		[Test]
        public void LoginName_IsSet()
		{
		    var postModel = new AuthLoginPostModel {LoginName = "login-name"};

            var sut = GetSut();
			var result = sut.Create(postModel);

			Assert.AreEqual(result.LoginName, "login-name");
		}

        private AuthLoginPageModelFactory GetSut()
        {
            return new AuthLoginPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object, 
                Mocks.WebContextMock.Object,
                Mocks.UrlProviderMock.Object);
        }

	}

}