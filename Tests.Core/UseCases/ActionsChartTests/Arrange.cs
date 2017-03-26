using Core.Entities;
using Core.Services;
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
            Mock<ICashgameService>().Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new ActionsChart.Request(CashgameData.Id1, PlayerData.Id1));
        }
    }
}