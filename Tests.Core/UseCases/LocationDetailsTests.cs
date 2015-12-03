using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class LocationDetailsTests : TestBase
    {
        [Test]
        public void LocationDetails_AllPropertiesAreSet()
        {
            var request = new LocationDetails.Request(TestData.UserA.UserName, TestData.LocationIdA);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.BunchA.Id, result.Id);
            Assert.AreEqual(TestData.LocationNameA, result.Name);
            Assert.AreEqual(TestData.BunchA.Slug, result.Slug);
        }

        private LocationDetails Sut
        {
            get
            {
                return new LocationDetails(
                    Services.LocationService,
                    Services.UserService,
                    Services.PlayerService,
                    Services.BunchService);
            }
        }
    }
}