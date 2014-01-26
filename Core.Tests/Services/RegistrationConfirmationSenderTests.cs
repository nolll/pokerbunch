using Core.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Core.Tests.Services
{
    public class RegistrationConfirmationSenderTests : MockContainer
    {
        [Test]
        public void Send_SendsMessageWithCorrectSubjectBody()
        {
            const string email = "a";
            const string subject = "b";
            const string body = "c";
            const string password = "d";
            var user = new FakeUser(email: email);
            GetMock<IRegistrationConfirmationMessageBuilder>().Setup(o => o.GetSubject()).Returns(subject);
            GetMock<IRegistrationConfirmationMessageBuilder>().Setup(o => o.GetBody(password)).Returns(body);

            var sut = GetSut();
            sut.Send(user, password);

            GetMock<IMessageSender>().Verify(o => o.Send(email, subject, body));
        }

        private RegistrationConfirmationSender GetSut()
        {
            return new RegistrationConfirmationSender(
                GetMock<IMessageSender>().Object,
                GetMock<IRegistrationConfirmationMessageBuilder>().Object);
        }
    }
}
