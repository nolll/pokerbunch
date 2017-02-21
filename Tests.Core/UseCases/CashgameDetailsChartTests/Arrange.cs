using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameDetailsChartTests
{
    public abstract class Arrange
    {
        protected CashgameDetailsChart Sut;

        [SetUp]
        public void Setup()
        {
            var cashgame = CashgameData.GameWithTwoPlayers(Role.Player);
            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);

            Sut = new CashgameDetailsChart(crm.Object);
        }

        protected CashgameDetailsChart.Request Request => new CashgameDetailsChart.Request(CashgameData.Id1);
    }
}
