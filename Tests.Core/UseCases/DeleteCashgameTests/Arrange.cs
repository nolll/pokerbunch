using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.DeleteCashgameTests
{
    public abstract class Arrange
    {
        protected DeleteCashgame Sut;
        protected const string IdWithResults = CashgameData.Id1;
        protected const string IdWithoutResults = CashgameData.Id2;
        protected abstract string Id { get; }
        protected string DeletedId;

        [SetUp]
        public void Setup()
        {
            var crm = new Mock<ICashgameRepository>();

            var cashgameWithResults = CashgameData.GameWithTwoPlayers(Role.Manager);
            crm.Setup(o => o.GetDetailedById(IdWithResults))
                .Returns(cashgameWithResults);

            var cashgameWithoutResults = CashgameData.GameWithoutPlayers(Role.Manager);
            crm.Setup(o => o.GetDetailedById(IdWithoutResults))
                .Returns(cashgameWithoutResults)
                .Callback((string id) => { DeletedId = id; });

            crm.Setup(o => o.DeleteGame(IdWithoutResults))
                .Callback((string id) => { DeletedId = id; });

            Sut = new DeleteCashgame(crm.Object);
        }

        protected DeleteCashgame.Request Request => new DeleteCashgame.Request(Id);
    }
}