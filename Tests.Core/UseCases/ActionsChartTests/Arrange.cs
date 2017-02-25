using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.ActionsChartTests
{
    public abstract class Arrange : UseCaseTest<ActionsChart>
    {
        protected ActionsChart.Result Result;

        protected virtual bool GameIsRunning => false; 

        protected override void Setup()
        {
            var cashgame = CashgameData.GameWithTwoPlayers(Role.Player, GameIsRunning);
            Mock<ICashgameRepository>().Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);
        }

        protected override void Execute()
        {
            Result = Sut.Execute(new ActionsChart.Request(CashgameData.Id1, PlayerData.Id1));
        }
    }
}