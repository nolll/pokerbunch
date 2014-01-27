using Application.Services;
using Application.Services.Interfaces;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services
{
    public class InvitationSenderTests : MockContainer
    {
        [Test]
        public void Send_SendsMessageWithCorrectSubjectBody()
        {
            const string email = "a";
            const string subject = "b";
            const string body = "c";
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            GetMock<IInvitationMessageBuilder>().Setup(o => o.GetSubject(homegame)).Returns(subject);
            GetMock<IInvitationMessageBuilder>().Setup(o => o.GetBody(homegame, player)).Returns(body);

            var sut = GetSut();
            sut.Send(homegame, player, email);

            GetMock<IMessageSender>().Verify(o => o.Send(email, subject, body));
        }

        private InvitationSender GetSut()
        {
            return new InvitationSender(
                GetMock<IMessageSender>().Object,
                GetMock<IInvitationMessageBuilder>().Object);
        }
    }
}
