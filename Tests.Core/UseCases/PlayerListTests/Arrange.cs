using Core.Entities;
using Core.Services;
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

            Mock<IBunchService>().Setup(o => o.Get(BunchId)).Returns(bunch);
            Mock<IPlayerService>().Setup(o => o.List(BunchId)).Returns(PlayerData.TwoPlayers);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new PlayerList.Request(BunchId));
        }
    }
}