using Application;
using Application.Services;
using Application.UseCases.TestEmail;
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
            Execute();

            GetMock<IMessageSender>().Verify(o => o.Send(Email, It.IsAny<IMessage>()));
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
