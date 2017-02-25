using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameDetailsTests
{
    public abstract class Arrange : UseCaseTest<CashgameDetails>
    {
        protected CashgameDetails.Result Result;

        protected abstract Role Role { get; }

        [SetUp]
        public void Setup()
        {
            var cashgame = CashgameData.GameWithTwoPlayers(Role);

            Mock<ICashgameRepository>().Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);

            Result = Sut.Execute(new CashgameDetails.Request(CashgameData.Id1));
        }
    }
}