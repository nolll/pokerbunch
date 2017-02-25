using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.LocationDetailsTests
{
    public abstract class Arrange : UseCaseTest<LocationDetails>
    {
        protected LocationDetails.Result Result;

        [SetUp]
        public void Setup()
        {
            var location = new Location(LocationData.Id1, LocationData.Name1, BunchData.Id1);

            Mock<ILocationRepository>().Setup(o => o.Get(LocationData.Id1)).Returns(location);

            Result = Sut.Execute(new LocationDetails.Request(LocationData.Id1));
        }
    }
}