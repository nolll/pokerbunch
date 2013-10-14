using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;
using Web.Models.UrlModels;

namespace Web.Tests.ModelFactoryTests.AuthModelFactories{

	public class AuthLoginPageModelFactoryTests : WebMockContainer
	{
		[Test]
        public void ReturnUrl_NoReturnUrl_IsSetToRoot()
		{
		    var sut = GetSut();

		    var result = sut.Create();

			Assert.AreEqual("/", result.ReturnUrl);
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
        public void AddUserUrl_IsSet(){
            var sut = GetSut();
			var result = sut.Create();

			Assert.IsInstanceOf<UserAddUrlModel>(result.AddUserUrl);
		}

		[Test]
        public void ForgotPasswordUrl_IsSet(){
            var sut = GetSut();
			var result = sut.Create();

			Assert.IsInstanceOf<ForgotPasswordUrlModel>(result.ForgotPasswordUrl);
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
            return new AuthLoginPageModelFactory(Mocks.PagePropertiesFactoryMock.Object, Mocks.WebContextMock.Object);
        }

	}

}