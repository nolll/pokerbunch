using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.DeleteCashgameTests
{
    public abstract class Arrange : UseCaseTest<DeleteCashgame>
    {
        protected DeleteCashgame.Result Result;

        protected const string IdWithResults = CashgameData.Id1;
        protected const string IdWithoutResults = CashgameData.Id2;
        protected abstract string Id { get; }
        protected string DeletedId;

        protected override void Setup()
        {
            var cashgameWithResults = CashgameData.GameWithTwoPlayers(Role.Manager);
            var cashgameWithoutResults = CashgameData.GameWithoutPlayers(Role.Manager);

            Mock<ICashgameRepository>().Setup(o => o.GetDetailedById(IdWithResults)).Returns(cashgameWithResults);
            Mock<ICashgameRepository>().Setup(o => o.GetDetailedById(IdWithoutResults)).Returns(cashgameWithoutResults).Callback((string id) => { DeletedId = id; });
            Mock<ICashgameRepository>().Setup(o => o.DeleteGame(IdWithoutResults)).Callback((string id) => { DeletedId = id; });
        }

        protected override void Execute()
        {
            Result = Sut.Execute(new DeleteCashgame.Request(Id));
        }
    }
}