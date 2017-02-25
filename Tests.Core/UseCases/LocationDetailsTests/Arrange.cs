using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.LocationDetailsTests
{
    public abstract class Arrange : UseCaseTest<LocationDetails>
    {
        protected LocationDetails.Result Result;

        protected override void Setup()
        {
            var location = new Location(LocationData.Id1, LocationData.Name1, BunchData.Id1);

            Mock<ILocationRepository>().Setup(o => o.Get(LocationData.Id1)).Returns(location);
        }

        protected override void Execute()
        {
            Result = Sut.Execute(new LocationDetails.Request(LocationData.Id1));
        }
    }
}