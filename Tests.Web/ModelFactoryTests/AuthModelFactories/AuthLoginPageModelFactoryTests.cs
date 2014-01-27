using Application.Services.Interfaces;
using Infrastructure.System;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.AuthModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.AuthModels;

namespace Tests.Web.ModelFactoryTests.AuthModelFactories{

	public class AuthLoginPageModelFactoryTests : MockContainer
	{
		[Test]
        public void ReturnUrl_NoReturnUrl_IsSetToRoot()
		{
            const string homeUrl = "a";
            GetMock<IUrlProvider>().Setup(o => o.GetHomeUrl()).Returns(homeUrl);

		    var sut = GetSut();

		    var result = sut.Create(null);

			Assert.AreEqual(homeUrl, result.ReturnUrl);
		}

		[Test]
        public void ReturnUrl_WithReturnUrl_IsSet()
		{
            GetMock<IWebContext>().Setup(o => o.GetQueryParam("return")).Returns("return-url");

		    var sut = GetSut();
            var result = sut.Create(null);

			Assert.AreEqual("return-url", result.ReturnUrl);
		}

		[Test]
        public void AddUserUrl_IsSet()
		{
		    const string loginUrl = "a";
		    GetMock<IUrlProvider>().Setup(o => o.GetAddUserUrl()).Returns(loginUrl);

            var sut = GetSut();
			var result = sut.Create(null);

			Assert.AreEqual(loginUrl, result.AddUserUrl);
		}

		[Test]
        public void ForgotPasswordUrl_IsSet(){
            const string forgotPasswordUrl = "a";
            GetMock<IUrlProvider>().Setup(o => o.GetForgotPasswordUrl()).Returns(forgotPasswordUrl);

            var sut = GetSut();
			var result = sut.Create(null);

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
                GetMock<IPagePropertiesFactory>().Object, 
                GetMock<IWebContext>().Object,
                GetMock<IUrlProvider>().Object);
        }

	}

}