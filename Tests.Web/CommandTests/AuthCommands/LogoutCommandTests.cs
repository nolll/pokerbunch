using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.AuthCommands;
using Web.Security;

namespace Tests.Web.CommandTests.AuthCommands
{
	public class LogoutCommandTests : MockContainer
	{
        [Test]
        public void Execute_ReturnsTrueAndSignsOut()
        {
            var sut = GetSut();
            var result = sut.Execute();

            Assert.IsTrue(result);
            GetMock<IAuthentication>().Verify(o => o.SignOut());
        }

        private LogoutCommand GetSut()
        {
            return new LogoutCommand(
                GetMock<IAuthentication>().Object);
        }
	}
}