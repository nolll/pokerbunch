using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BuyinTests
{
    public abstract class Arrange : UseCaseTest<Buyin>
    {
        protected const string CashgameId = CashgameData.Id1;
        private const string BunchId = BunchData.Id1;
        protected const string PlayerId = PlayerData.Id1;
        protected const int AddedAmount = 200;
        protected const int Stack = 100;

        protected string PostedCashgameId;
        protected string PostedPlayerId;
        protected int PostedAmount;
        protected int PostedStack;

        protected override void Setup()
        {
            PostedCashgameId = null;
            PostedPlayerId = null;
            PostedAmount = 0;
            PostedStack = 0;

            Mock<ICashgameRepository>().Setup(o => o.GetCurrent(BunchId)).Returns(CashgameData.CreateDetailed(CashgameId));

            Mock<ICashgameRepository>().Setup(o => o.Buyin(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Callback((string cashgameId, string playerId, int added, int stack) =>
                {
                    PostedCashgameId = cashgameId;
                    PostedPlayerId = playerId;
                    PostedAmount = added;
                    PostedStack = stack;
                });
        }

        protected override void Execute()
        {
            Subject.Execute(new Buyin.Request(BunchId, PlayerId, AddedAmount, Stack));
        }
    }
}