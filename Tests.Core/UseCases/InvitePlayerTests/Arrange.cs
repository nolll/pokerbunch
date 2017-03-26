using Core.Services;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.InvitePlayerTests
{
    public abstract class Arrange : UseCaseTest<InvitePlayer>
    {
        protected InvitePlayer.Result Result;

        protected const string PlayerId = PlayerData.Id1;
        protected const string Email = UserData.Email1;

        protected string PostedPlayerId;
        protected string PostedEmail;

        protected override void Setup()
        {
            PostedPlayerId = null;
            PostedEmail = null;

            Mock<IPlayerService>().Setup(o => o.Invite(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string playerId, string email) => { PostedPlayerId = playerId; PostedEmail = email; });
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new InvitePlayer.Request(PlayerId, Email));
        }
    }
}