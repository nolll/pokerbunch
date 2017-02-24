using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerListTests
{
    public abstract class Arrange
    {
        protected PlayerList Sut;

        private const string BunchId = BunchData.Id1;
        protected virtual Role Role => Role.Player;

        [SetUp]
        public void Setup()
        {
            var bunch = new Bunch(BunchId, role: Role);
            var brm = new Mock<IBunchRepository>();
            brm.Setup(o => o.Get(BunchId)).Returns(bunch);

            var prm = new Mock<IPlayerRepository>();
            prm.Setup(o => o.List(BunchId)).Returns(PlayerData.TwoPlayers);

            Sut = new PlayerList(brm.Object, prm.Object);
        }

        protected PlayerList.Request Request => new PlayerList.Request(BunchId);
    }
}