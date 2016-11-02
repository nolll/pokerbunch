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
            var request = new LocationDetails.Request(TestData.LocationIdA);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.LocationIdA, result.Id);
            Assert.AreEqual(TestData.LocationNameA, result.Name);
            Assert.AreEqual(TestData.BunchA.Id, result.Slug);
        }

        private LocationDetails Sut => new LocationDetails(
            Deps.Location);
    }
}