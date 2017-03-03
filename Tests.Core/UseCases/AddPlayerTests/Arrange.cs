using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddPlayerTests
{
    public class Arrange : UseCaseTest<AddPlayer>
    {
        protected AddPlayer.Result Result;

        protected Player Added;

        protected const string BunchId = BunchData.Id1;
        private const string PlayerName = PlayerData.Name1;

        protected override void Setup()
        {
            Added = null;

            Mock<IPlayerRepository>().Setup(o => o.Add(It.IsAny<Player>()))
                .Callback((Player p) => Added = p);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new AddPlayer.Request(BunchId, PlayerName));
        }
    }
}