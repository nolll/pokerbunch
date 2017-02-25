using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
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
            var playerThatHasNotPlayed = new Player(BunchData.Id1, IdForPlayerThatHasPlayed, null);
            var playerThatHasPlayed = new Player(BunchData.Id1, IdForPlayerThatHasNotPlayed, null);
            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var cashgameWithoutResults = new List<ListCashgame>();

            Mock<IPlayerRepository>().Setup(o => o.Get(IdForPlayerThatHasNotPlayed)).Returns(playerThatHasNotPlayed);
            Mock<IPlayerRepository>().Setup(o => o.Get(IdForPlayerThatHasPlayed)).Returns(playerThatHasPlayed);
            Mock<IPlayerRepository>().Setup(o => o.Delete(IdForPlayerThatHasNotPlayed)).Callback((string id) => { DeletedId = id; });
            Mock<ICashgameRepository>().Setup(o => o.PlayerList(IdForPlayerThatHasPlayed)).Returns(cashgames);
            Mock<ICashgameRepository>().Setup(o => o.PlayerList(IdForPlayerThatHasNotPlayed)).Returns(cashgameWithoutResults);
        }

        protected override void Execute()
        {
            Result = Sut.Execute(new DeletePlayer.Request(PlayerId));
        }
    }
}