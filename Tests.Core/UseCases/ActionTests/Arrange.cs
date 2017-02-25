using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.ActionTests
{
    public abstract class Arrange : UseCaseTest<Actions>
    {
        protected Actions.Result Result;

        protected abstract Role Role { get; }

        [SetUp]
        public void Setup()
        {
            var cashgame = CashgameData.GameWithTwoPlayers(Role);

            Mock<ICashgameRepository>().Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);

            Result = Sut.Execute(new Actions.Request(CashgameData.Id1, PlayerData.Id1));
        }
    }
}
