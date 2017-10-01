using Core.Entities;
using Core.Services;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddCashgameFormTests
{
    public abstract class Arrange : UseCaseTest<AddCashgameForm>
    {
        protected AddCashgameForm.Result Result;

        private const string BunchIdWithoutRunningGame = BunchData.Id1;
        protected const string BunchIdWithRunningGame = BunchData.Id2;
        protected virtual string BunchId => BunchIdWithoutRunningGame;

        protected override void Setup()
        {
            var bunch = new Bunch(BunchId);
            var cashgame = CashgameData.GameWithoutPlayers(Role.Player);
            var locations = LocationData.TwoLocations;
            var events = EventData.TwoEvents;

            Mock<IBunchService>().Setup(o => o.Get(BunchId)).Returns(bunch);
            Mock<ICashgameService>().Setup(o => o.GetCurrent(BunchIdWithRunningGame)).Returns(cashgame);
            Mock<ILocationService>().Setup(o => o.List(BunchId)).Returns(locations);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new AddCashgameForm.Request(BunchId));
        }
    }
}