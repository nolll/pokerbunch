using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AddLocationTests
{
    public class Arrange : UseCaseTest<AddLocation>
    {
        protected AddLocation.Result Result;
        protected Location Added;

        protected string BunchId = BunchData.Id1;
        protected string LocationName = LocationData.Name1;

        protected override void Setup()
        {
            Added = null;

            Mock<ILocationRepository>().Setup(o => o.Add(It.IsAny<Location>()))
                .Callback((Location location) => Added = location);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new AddLocation.Request(BunchId, LocationName));
        }
    }
}