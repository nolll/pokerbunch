using Core;
using Core.Services.Interfaces;
using Core.UseCases.TestEmail;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class TestEmailTests : TestBase
    {
        private const string Email = "henriks@gmail.com";

        [Test]
        public void TestEmail_MessageIsSent()
        {
            string email = null;
            IMessage message = null;

            GetMock<IMessageSender>()
                .Setup(o => o.Send(It.IsAny<string>(), It.IsAny<IMessage>()))
                .Callback((string e, IMessage m) =>
                {
                    email = e;
                    message = m;
                });
            
            Execute();

            Assert.AreEqual(Email, email);
            Assert.AreEqual("Test Email", message.Subject);
            Assert.AreEqual("This is a test email from pokerbunch.com", message.Body);
        }

        [Test]
        public void TestEmail_EmailIsSet()
        {
            var result = Execute();

            Assert.AreEqual(Email, result.Email);
        }

        private TestEmailResult Execute()
        {
            return TestEmailInteractor.Execute(GetMock<IMessageSender>().Object);
        }
    }
}
