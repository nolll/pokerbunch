using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashoutTests
{
    public abstract class Arrange : UseCaseTest<Cashout>
    {
        private const string BunchId = BunchData.Id1;
        protected const string CashgameId = CashgameData.Id1;
        protected const string PlayerId = PlayerData.Id1;
        protected const int CashoutStack = 123;

        protected string PostedCashgameId;
        protected string PostedPlayerId;
        protected int PostedStack;

        protected override void Setup()
        {
            PostedCashgameId = null;
            PostedPlayerId = null;
            PostedStack = 0;

            var cashgame = CashgameData.CreateDetailed(CashgameId);

            Mock<ICashgameRepository>().Setup(s => s.GetCurrent(BunchData.Id1)).Returns(cashgame);
            Mock<ICashgameRepository>().Setup(o => o.Cashout(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Callback((string cashgameId, string playerId, int stack) => { PostedCashgameId = cashgameId; PostedPlayerId = playerId; PostedStack = stack; });
        }

        protected override void Execute()
        {
            Subject.Execute(new Cashout.Request(BunchId, PlayerId, CashoutStack));
        }
    }
}