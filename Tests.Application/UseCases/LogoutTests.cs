using Core.Services;
using Core.Urls;
using Core.UseCases.Logout;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class LogoutTests : TestBase
    {
        [Test]
        public void Logout_SignsOut()
        {
            Execute();

            GetMock<IAuth>().Verify(o => o.SignOut());
        }

        [Test]
        public void Logout_ReturnUrlIsSet()
        {
            var result = Execute();

            Assert.IsInstanceOf<HomeUrl>(result.ReturnUrl);
        }

        private LogoutResult Execute()
        {
            return LogoutInteractor.Execute(GetMock<IAuth>().Object);
        }
    }
}
