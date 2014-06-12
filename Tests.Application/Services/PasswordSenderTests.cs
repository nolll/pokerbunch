using Application.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services
{
    public class PasswordSenderTests : MockContainer
    {
        [Test]
        public void Send_SendsMessageWithCorrectSubjectBody()
        {
            const string email = "a";
            const string subject = "b";
            const string body = "c";
            const string password = "d";
            var user = new UserInTest(email: email);
            GetMock<IPasswordMessageBuilder>().Setup(o => o.GetSubject()).Returns(subject);
            GetMock<IPasswordMessageBuilder>().Setup(o => o.GetBody(password)).Returns(body);

            var sut = GetSut();
            sut.Send(user, password);

            GetMock<IMessageSender>().Verify(o => o.Send(email, subject, body));
        }

        private PasswordSender GetSut()
        {
            return new PasswordSender(
                GetMock<IMessageSender>().Object,
                GetMock<IPasswordMessageBuilder>().Object);
        }
    }
}
