using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddCashgameFormTests
{
    public abstract class Arrange : UseCaseTest<AddCashgameForm>
    {
        private const string BunchIdWithoutRunningGame = BunchData.Id1;
        protected const string BunchIdWithRunningGame = BunchData.Id2;
        protected virtual string BunchId => BunchIdWithoutRunningGame;

        [SetUp]
        public void Setup()
        {
            var bunch = new Bunch(BunchId);
            var cashgame = CashgameData.GameWithoutPlayers(Role.Player);
            var locations = LocationData.TwoLocations;
            var events = EventData.TwoEvents;

            Mock<IBunchRepository>().Setup(o => o.Get(BunchId)).Returns(bunch);
            Mock<ICashgameRepository>().Setup(o => o.GetCurrent(BunchIdWithRunningGame)).Returns(cashgame);
            Mock<ILocationRepository>().Setup(o => o.List(BunchId)).Returns(locations);
            Mock<IEventRepository>().Setup(o => o.ListByBunch(BunchId)).Returns(events);
        }

        protected AddCashgameForm.Result Execute() => Sut.Execute(new AddCashgameForm.Request(BunchId));
    }
}