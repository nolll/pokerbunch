using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.LocationDetailsTests
{
    public abstract class Arrange : UseCaseTest<PlayerList>
    {
        [SetUp]
        public void Setup()
        {
            var lrm = new Mock<ILocationRepository>();
            var location = new Location(LocationData.Id1, LocationData.Name1, BunchData.Id1);
            lrm.Setup(o => o.Get(LocationData.Id1)).Returns(location);

            Sut = new LocationDetails(lrm.Object);
        }

        protected LocationDetails.Request Request => new LocationDetails.Request(LocationData.Id1);
    }

    public class LocationDetailsTests1 : Arrange
    {
        [Test]
        public void LocationDetails_AllPropertiesAreSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(LocationData.Id1, result.Id);
            Assert.AreEqual(LocationData.Name1, result.Name);
            Assert.AreEqual(BunchData.Id1, result.Slug);
        }
    }
}