using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerListTests
{
    public abstract class Arrange : UseCaseTest<PlayerList>
    {
        protected PlayerList.Result Result;

        private const string BunchId = BunchData.Id1;
        protected virtual Role Role => Role.Player;

        protected override void Setup()
        {
            var bunch = new Bunch(BunchId, role: Role);

            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(bunch);
            Mock<IPlayerRepository>().Setup(o => o.List(BunchId)).Returns(PlayerData.TwoPlayers);
        }

        protected override void Execute()
        {
            Result = Sut.Execute(new PlayerList.Request(BunchId));
        }
    }
}