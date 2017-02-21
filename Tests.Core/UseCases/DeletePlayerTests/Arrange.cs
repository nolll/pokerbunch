using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.DeletePlayerTests
{
    public abstract class Arrange
    {
        protected DeletePlayer Sut;

        protected string IdForPlayerThatHasPlayed = PlayerData.Id1;
        protected string IdForPlayerThatHasNotPlayed = PlayerData.Id2;
        protected abstract string PlayerId { get; }
        protected string DeletedId;

        [SetUp]
        public void Setup()
        {
            var prm = new Mock<IPlayerRepository>();
            prm.Setup(o => o.Get(IdForPlayerThatHasNotPlayed))
                .Returns(new Player(BunchData.Id1, IdForPlayerThatHasNotPlayed, null));
            prm.Setup(o => o.Get(IdForPlayerThatHasPlayed))
                .Returns(new Player(BunchData.Id1, IdForPlayerThatHasPlayed, null));
            prm.Setup(o => o.Delete(IdForPlayerThatHasNotPlayed))
                .Callback((string id) => { DeletedId = id; });

            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.PlayerList(IdForPlayerThatHasPlayed))
                .Returns(cashgames);

            var cashgameWithoutResults = new List<ListCashgame>();
            crm.Setup(o => o.PlayerList(IdForPlayerThatHasNotPlayed))
                .Returns(cashgameWithoutResults);

            Sut = new DeletePlayer(prm.Object, crm.Object);
        }

        protected DeletePlayer.Request Request => new DeletePlayer.Request(PlayerId);
    }
}