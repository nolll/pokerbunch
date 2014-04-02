using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.AuthCommands;
using Web.Security;
using Web.Services;

namespace Tests.Web.CommandTests.AuthCommands{

	public class LogoutCommandTests : MockContainer
	{
        [Test]
        public void Execute_ReturnsTrueAndClearsCookies()
        {
            const string cookieName = "token";

            var sut = GetSut();
            var result = sut.Execute();

            Assert.IsTrue(result);
            GetMock<IWebContext>().Verify(o => o.ClearCookie(cookieName));
        }

        [Test]
        public void Execute_ReturnsTrueAndSignsOut()
        {
            var sut = GetSut();
            var result = sut.Execute();

            Assert.IsTrue(result);
            GetMock<IAuthenticationService>().Verify(o => o.SignOut());
        }

        private LogoutCommand GetSut()
        {
            return new LogoutCommand(
                GetMock<IWebContext>().Object,
                GetMock<IAuthenticationService>().Object);
        }

	}

}