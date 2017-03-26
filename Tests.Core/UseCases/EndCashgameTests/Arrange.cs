using Core.Services;
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

            var cashgame = CashgameData.CreateDetailed(CashgameId);
            Mock<ICashgameService>().Setup(o => o.GetCurrent(BunchId)).Returns(cashgame);
            Mock<ICashgameService>().Setup(o => o.End(It.IsAny<string>())).Callback((string cashgameId) => PostedCashgameId = cashgameId);
        }

        protected override void Execute()
        {
            Subject.Execute(new EndCashgame.Request(BunchId));
        }
    }
}