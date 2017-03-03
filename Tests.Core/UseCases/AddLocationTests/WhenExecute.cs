using NUnit.Framework;

namespace Tests.Core.UseCases.AddLocationTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void LocationIsAdded() => Assert.AreEqual(LocationName, Added.Name);

        [Test]
        public void BunchIdIsSet() => Assert.AreEqual(BunchId, Result.Slug);
    }
}