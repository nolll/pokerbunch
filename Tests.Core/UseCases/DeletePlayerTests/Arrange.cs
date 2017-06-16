using System.Collections.Generic;
using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.DeletePlayerTests
{
    public abstract class Arrange : UseCaseTest<DeletePlayer>
    {
        protected DeletePlayer.Result Result;

        protected string IdForPlayerThatHasPlayed = PlayerData.Id1;
        protected string IdForPlayerThatHasNotPlayed = PlayerData.Id2;
        protected abstract string PlayerId { get; }
        protected string DeletedId;

        protected override void Setup()
        {
            var playerThatHasNotPlayed = new Player(BunchData.Id1, IdForPlayerThatHasPlayed, null, null);
            var playerThatHasPlayed = new Player(BunchData.Id1, IdForPlayerThatHasNotPlayed, null, null);
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var cashgameWithoutResults = new List<ListCashgame>();

            Mock<IPlayerService>().Setup(o => o.Get(IdForPlayerThatHasNotPlayed)).Returns(playerThatHasNotPlayed);
            Mock<IPlayerService>().Setup(o => o.Get(IdForPlayerThatHasPlayed)).Returns(playerThatHasPlayed);
            Mock<IPlayerService>().Setup(o => o.Delete(IdForPlayerThatHasNotPlayed)).Callback((string id) => { DeletedId = id; });
            Mock<ICashgameService>().Setup(o => o.PlayerList(IdForPlayerThatHasPlayed)).Returns(cashgames);
            Mock<ICashgameService>().Setup(o => o.PlayerList(IdForPlayerThatHasNotPlayed)).Returns(cashgameWithoutResults);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new DeletePlayer.Request(PlayerId));
        }
    }
}