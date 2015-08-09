using Core.Urls;
using Core.UseCases.LoginForm;
using NUnit.Framework;

namespace Tests.Core.UseCases
{
    class LoginFormTests
    {
        [Test]
        public void LoginForm_ForgotPasswordUrlIsSet()
        {
            var request = new LoginFormRequest("");

            var result = Sut.Execute(request);

            Assert.IsInstanceOf<ForgotPasswordUrl>(result.ForgotPasswordUrl);
        }

        [Test]
        public void LoginForm_WithoutReturnUrl_ReturnUrlHomeUrl()
        {
            var request = new LoginFormRequest("");

            var result = Sut.Execute(request);

            Assert.IsInstanceOf<HomeUrl>(result.ReturnUrl);
        }

        [Test]
        public void LoginForm_WithReturnUrl_ReturnUrlIsSet()
        {
            const string resultUrl = "/a";
            var request = new LoginFormRequest(resultUrl);

            var result = Sut.Execute(request);

            Assert.AreEqual(resultUrl, result.ReturnUrl.Relative);
        }

        private LoginFormInteractor Sut
        {
            get { return new LoginFormInteractor(); }
        }
    }
}
