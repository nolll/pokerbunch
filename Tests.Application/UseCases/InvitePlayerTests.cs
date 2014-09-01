using System.Linq;
using Application.Services;
using Application.Urls;
using Application.UseCases.InvitePlayer;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class InvitePlayerTests : MockContainer
    {
        private const string Slug = "a";
        private const string BunchName = "b";
        private const string PlayerName = "c";
        private const string ValidEmail = "a@b.com";
        private const int PlayerId = 1;

        [Test]
        public void InvitePlayer_ReturnUrlIsSet()
        {
            var request = CreateRequest();

            SetupBunch();
            SetupPlayer();

            var result = Sut.Execute(request);

            Assert.IsInstanceOf<InvitePlayerConfirmationUrl>(result.ReturnUrl);
        }

        [TestCase("")]
        [TestCase("a")]
        public void InvitePlayer_InvalidEmail_SuccessIsFalseAndErrorListContainsErrors(string email)
        {
            var request = CreateRequest(email);

            var result = Sut.Execute(request);

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void InvitePlayer_ValidEmail_SuccessIsTrue()
        {
            var request = CreateRequest();

            SetupBunch();
            SetupPlayer();

            var result = Sut.Execute(request);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.Errors.Count());
        }

        [Test]
        public void InvitePlayer_ValidEmail_SendsInvitationEmail()
        {
            const string subject = "Invitation to Poker Bunch: b";
            const string body = @"You have been invited to join the poker game: b.

To accept this invitation, go to http://pokerbunch.com/a/homegame/join and enter this verification code: efebc685cd6c0f3801f129748c5d74d6592d1bfe

If you don't have an account, you can register at http://pokerbunch.com/-/user/add";
            var request = CreateRequest();

            SetupBunch();
            SetupPlayer();

            Sut.Execute(request);

            GetMock<IMessageSender>().Verify(o => o.Send(ValidEmail, subject, body));
        }

        private static InvitePlayerRequest CreateRequest(string email = ValidEmail)
        {
            return new InvitePlayerRequest(Slug, PlayerId, email);
        }

        private void SetupBunch()
        {
            var bunch = new BunchInTest(slug: Slug, displayName: BunchName);
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
        }

        private void SetupPlayer()
        {
            var player = new PlayerInTest(displayName: PlayerName);
            GetMock<IPlayerRepository>().Setup(o => o.GetById(PlayerId)).Returns(player);
        }

        private InvitePlayerInteractor Sut
        {
            get
            {
                return new InvitePlayerInteractor(
                    GetMock<IMessageSender>().Object,
                    GetMock<IBunchRepository>().Object,
                    GetMock<IPlayerRepository>().Object);
            }
        }
    }
}
