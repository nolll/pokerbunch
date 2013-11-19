using NUnit.Framework;
using Tests.Common;
using Web.Commands.AuthCommands;

namespace Web.Tests.CommandTests.AuthCommands{

	public class LogoutCommandTests : MockContainer
	{
        [Test]
        public void Execute_ReturnsTrueAndClearsCookies()
        {
            const string cookieName = "token";

            var sut = GetSut();
            var result = sut.Execute();

            Assert.IsTrue(result);
            Mocks.WebContextMock.Verify(o => o.ClearCookie(cookieName));
        }

        private LogoutCommand GetSut()
        {
            return new LogoutCommand(
                Mocks.WebContextMock.Object);
        }

	}

}