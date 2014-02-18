using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.AdminCommands;

namespace Tests.Web.CommandTests.AdminTests
{
    public class EmailTestCommandTests : MockContainer
    {
        [Test]
        public void Execute_MessageSenderIsCalledWithCorrectParameters()
        {
            const string email = "a";
            const string subject = "Test Email";
            const string body = "This is a test email from pokerbunch.com";

            var sut = GetSut(email);
            var result = sut.Execute();

            Assert.IsTrue(result);
            GetMock<IMessageSender>().Verify(o => o.Send(email, subject, body));
        }

        private EmailTestCommand GetSut(string email)
        {
            return new EmailTestCommand(
                GetMock<IMessageSender>().Object,
                email);
        }
    }
}
