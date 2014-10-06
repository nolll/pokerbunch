using Core.Urls;
using Core.UseCases.LoginForm;
using NUnit.Framework;

namespace Tests.Application.UseCases
{
    class LoginFormTests
    {
        [Test]
        public void LoginForm_AddUserUrlIsSet()
        {
            var request = new LoginFormRequest("");
            
            var result = Execute(request);

            Assert.IsInstanceOf<AddUserUrl>(result.AddUserUrl);
        }

        [Test]
        public void LoginForm_ForgotPasswordUrlIsSet()
        {
            var request = new LoginFormRequest("");

            var result = Execute(request);

            Assert.IsInstanceOf<ForgotPasswordUrl>(result.ForgotPasswordUrl);
        }

        [Test]
        public void LoginForm_WithoutReturnUrl_ReturnUrlHomeUrl()
        {
            var request = new LoginFormRequest("");

            var result = Execute(request);

            Assert.IsInstanceOf<HomeUrl>(result.ReturnUrl);
        }

        [Test]
        public void LoginForm_WithReturnUrl_ReturnUrlIsSet()
        {
            const string resultUrl = "/a";
            var request = new LoginFormRequest(resultUrl);

            var result = Execute(request);

            Assert.AreEqual(resultUrl, result.ReturnUrl.Relative);
        }
        
        private LoginFormResult Execute(LoginFormRequest request)
        {
            return LoginFormInteractor.Execute(request);
        }
    }
}
