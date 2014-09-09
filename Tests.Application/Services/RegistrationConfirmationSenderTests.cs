using Application.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services
{
    public class RegistrationConfirmationSenderTests : TestBase
    {
        [Test]
        public void Send_SendsMessage()
        {
            const string email = "a";
            const string password = "d";
            var user = new UserInTest(email: email);

            var sut = GetSut();
            sut.Send(user, password);

            GetMock<IMessageSender>().Verify(o => o.Send(email, It.IsAny<string>(), It.IsAny<string>()));
        }

        private RegistrationConfirmationSender GetSut()
        {
            return new RegistrationConfirmationSender(
                GetMock<IMessageSender>().Object);
        }
    }
}
