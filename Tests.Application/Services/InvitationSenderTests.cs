using Application.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services
{
    public class InvitationSenderTests : MockContainer
    {
        [Test]
        public void Send_SendsMessage()
        {
            const string email = "a";
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();

            var sut = GetSut();
            sut.Send(homegame, player, email);

            GetMock<IMessageSender>().Verify(o => o.Send(email, It.IsAny<string>(), It.IsAny<string>()));
        }

        private InvitationSender GetSut()
        {
            return new InvitationSender(
                GetMock<IMessageSender>().Object);
        }
    }
}
