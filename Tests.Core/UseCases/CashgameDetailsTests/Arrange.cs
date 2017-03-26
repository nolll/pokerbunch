using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameDetailsTests
{
    public abstract class Arrange : UseCaseTest<CashgameDetails>
    {
        protected CashgameDetails.Result Result;

        protected abstract Role Role { get; }

        protected override void Setup()
        {
            var cashgame = CashgameData.GameWithTwoPlayers(Role);

            Mock<ICashgameService>().Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new CashgameDetails.Request(CashgameData.Id1));
        }
    }
}