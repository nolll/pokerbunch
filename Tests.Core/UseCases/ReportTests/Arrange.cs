using Core.Services;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.ReportTests
{
    public abstract class Arrange : UseCaseTest<Report>
    {
        private const string BunchId = BunchData.Id1;
        protected const string CashgameId = CashgameData.Id1;
        protected const string PlayerId = PlayerData.Id1;
        protected const int Stack = 1;

        protected string PostedCashgameId;
        protected string PostedPlayerId;
        protected int PostedStack;

        protected override void Setup()
        {
            PostedCashgameId = null;
            PostedPlayerId = null;
            PostedStack = 0;

            var cashgame = CashgameData.CreateDetailed(CashgameId);
            Mock<ICashgameService>().Setup(o => o.GetCurrent(BunchId)).Returns(cashgame);

            Mock<ICashgameService>().Setup(o => o.Report(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Callback((string cashgameId, string playerId, int stack) =>
                {
                    PostedCashgameId = cashgameId;
                    PostedPlayerId = playerId;
                    PostedStack = stack;
                });
        }

        protected override void Execute()
        {
            Subject.Execute(new Report.Request(BunchId, PlayerId, Stack));
        }
    }
}