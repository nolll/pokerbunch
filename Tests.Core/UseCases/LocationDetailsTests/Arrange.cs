using Core.Entities;
using Core.Services;
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

            Mock<ILocationService>().Setup(o => o.Get(LocationData.Id1)).Returns(location);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new LocationDetails.Request(LocationData.Id1));
        }
    }
}