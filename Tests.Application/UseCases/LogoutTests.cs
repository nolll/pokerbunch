using Application.Services;
using Application.Urls;
using Application.UseCases.Logout;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class LogoutTests : MockContainer
    {
        [Test]
        public void Logout_SignsOut()
        {
            Sut.Execute();

            GetMock<IAuth>().Verify(o => o.SignOut());
        }

        [Test]
        public void Logout_ReturnUrlIsSet()
        {
            var result = Sut.Execute();

            Assert.IsInstanceOf<HomeUrl>(result.ReturnUrl);
        }

        private LogoutInteractor Sut
        {
            get { return new LogoutInteractor(
                GetMock<IAuth>().Object); }
        }
    }
}
