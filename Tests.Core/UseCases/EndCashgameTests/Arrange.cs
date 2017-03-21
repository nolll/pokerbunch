using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EndCashgameTests
{
    public abstract class Arrange : UseCaseTest<EndCashgame>
    {
        private const string BunchId = BunchData.Id1;
        protected const string CashgameId = CashgameData.Id1;

        protected string PostedCashgameId;

        protected override void Setup()
        {
            PostedCashgameId = null;

            var cashgame = new Cashgame(BunchId, LocationData.Id1, GameStatus.Running, CashgameId);
            Mock<ICashgameRepository>().Setup(o => o.GetRunning(BunchId)).Returns(cashgame);
            Mock<ICashgameRepository>().Setup(o => o.End(It.IsAny<string>())).Callback((string cashgameId) => PostedCashgameId = cashgameId);
        }

        protected override void Execute()
        {
            Subject.Execute(new EndCashgame.Request(BunchId));
        }
    }
}