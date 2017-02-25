using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.LocationDetailsTests
{
    public class WithLocation : Arrange
    {
        [Test]
        public void AllPropertiesAreSet()
        {
            Assert.AreEqual(LocationData.Id1, Result.Id);
            Assert.AreEqual(LocationData.Name1, Result.Name);
            Assert.AreEqual(BunchData.Id1, Result.Slug);
        }
    }
}