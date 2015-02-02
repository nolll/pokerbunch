using Core.Urls;
using Core.UseCases.Logout;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class LogoutTests : TestBase
    {
        [Test]
        public void Logout_SignsOut()
        {
            Sut.Execute();

            Assert.IsTrue(Services.Auth.SignedOut);
        }

        [Test]
        public void Logout_ReturnUrlIsSet()
        {
            var result = Sut.Execute();

            Assert.IsInstanceOf<HomeUrl>(result.ReturnUrl);
        }

        private LogoutInteractor Sut
        {
            get { return new LogoutInteractor(Services.Auth); }
        }
    }
}
