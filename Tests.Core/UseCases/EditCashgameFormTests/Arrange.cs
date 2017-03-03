using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditCashgameFormTests
{
    public abstract class Arrange : UseCaseTest<EditCashgameForm>
    {
        protected EditCashgameForm.Result Result;

        protected abstract Role Role { get; }
        
        protected override void Setup()
        {
            var cashgame = CashgameData.GameWithTwoPlayers(Role);
            var locations = LocationData.TwoLocations;

            Mock<ICashgameRepository>().Setup(o => o.GetDetailedById(CashgameData.Id1)).Returns(cashgame);
            Mock<ILocationRepository>().Setup(o => o.List(BunchData.Id1)).Returns(locations);
            Mock<IEventRepository>().Setup(o => o.ListByBunch(BunchData.Id1)).Returns(EventData.TwoEvents);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new EditCashgameForm.Request(CashgameData.Id1));
        }
    }
}