using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameDetailsChartTests
{
    public abstract class Arrange : UseCaseTest<CashgameDetailsChart>
    {
        protected CashgameDetailsChart.Result Result;

        protected override void Setup()
        {
            var cashgame = CashgameData.GameWithTwoPlayers(Role.Player);

            Mock<ICashgameRepository>().Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new CashgameDetailsChart.Request(CashgameData.Id1));
        }
    }
}
