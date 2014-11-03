using System.Linq;
using Core;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using Core.UseCases.InvitePlayer;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class InvitePlayerTests : TestBase
    {
        private const string PlayerName = "c";
        private const string ValidEmail = "a@b.com";
        private const int PlayerId = 1;

        [Test]
        public void InvitePlayer_ReturnUrlIsSet()
        {
            var request = CreateRequest();

            SetupPlayer();

            var result = Execute(request);

            Assert.IsInstanceOf<InvitePlayerConfirmationUrl>(result.ReturnUrl);
        }

        [TestCase("")]
        [TestCase("a")]
        public void InvitePlayer_InvalidEmail_ThrowsException(string email)
        {
            var request = CreateRequest(email);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void InvitePlayer_ValidEmail_SendsInvitationEmail()
        {
            const string subject = "Invitation to Poker Bunch: Bunch A";
            const string body = @"You have been invited to join the poker game: Bunch A.

To accept this invitation, go to http://pokerbunch.com/bunch-a/homegame/join and enter this verification code: efebc685cd6c0f3801f129748c5d74d6592d1bfe

If you don't have an account, you can register at http://pokerbunch.com/-/user/add";
            var request = CreateRequest();

            SetupPlayer();

            string email = null;
            IMessage message = null;
            GetMock<IMessageSender>()
                .Setup(o => o.Send(It.IsAny<string>(), It.IsAny<IMessage>()))
                .Callback((string e, IMessage m) =>
                {
                    email = e;
                    message = m;
                });

            Execute(request);

            Assert.AreEqual(ValidEmail, email);
            Assert.AreEqual(subject, message.Subject);
            Assert.AreEqual(body, message.Body);
        }

        private InvitePlayerResult Execute(InvitePlayerRequest request)
        {
            return InvitePlayerInteractor.Execute(
                Repo.Bunch,
                GetMock<IPlayerRepository>().Object,
                GetMock<IMessageSender>().Object,
                request);
        }

        private static InvitePlayerRequest CreateRequest(string email = ValidEmail)
        {
            return new InvitePlayerRequest(Constants.SlugA, PlayerId, email);
        }

        private void SetupPlayer()
        {
            var player = A.Player.WithDisplayName(PlayerName).Build();
            GetMock<IPlayerRepository>().Setup(o => o.GetById(PlayerId)).Returns(player);
        }
    }
}
