using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.ActionsChartTests
{
    public abstract class Arrange
    {
        protected ActionsChart Sut;

        protected virtual bool GameIsRunning => false; 

        [SetUp]
        public void Setup()
        {
            var crm = new Mock<ICashgameRepository>();
            var cashgame = CashgameData.EndedGameWithTwoPlayers(Role.Player, GameIsRunning);
            crm.Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);

            Sut = new ActionsChart(crm.Object);
        }

        protected ActionsChart.Request Request => new ActionsChart.Request(CashgameData.Id1, PlayerData.Id1);
    }
}