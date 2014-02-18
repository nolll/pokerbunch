using Application.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.AdminCommands;

namespace Tests.Web.CommandTests.AdminTests
{
    public class AdminCommandProviderTests : MockContainer
    {
        [Test]
        public void GetEmailTestCommand_ReturnsCommandOfCorrectType()
        {
            var sut = GetSut();
            var result = sut.GetEmailTestCommand(It.IsAny<string>());

            Assert.IsInstanceOf<EmailTestCommand>(result);
        }

        private AdminCommandProvider GetSut()
        {
            return new AdminCommandProvider(
                GetMock<IMessageSender>().Object);
        }
    }
}