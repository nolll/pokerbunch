using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchListTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void BunchList_ReturnsListOfBunchItems()
        {
            Assert.AreEqual(2, Result.Bunches.Count);
            Assert.AreEqual(BunchData.Id1, Result.Bunches[0].Slug);
            Assert.AreEqual(BunchData.DisplayName1, Result.Bunches[0].DisplayName);
            Assert.AreEqual(BunchData.Id2, Result.Bunches[1].Slug);
            Assert.AreEqual(BunchData.DisplayName2, Result.Bunches[1].DisplayName);
        }
    }
}
