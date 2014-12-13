using Core.Services;
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
            Execute();

            Assert.IsTrue(Services.Auth.SignedOut);
        }

        [Test]
        public void Logout_ReturnUrlIsSet()
        {
            var result = Execute();

            Assert.IsInstanceOf<HomeUrl>(result.ReturnUrl);
        }

        private LogoutResult Execute()
        {
            return LogoutInteractor.Execute(Services.Auth);
        }
    }
}
