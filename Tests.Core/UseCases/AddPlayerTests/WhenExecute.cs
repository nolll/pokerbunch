using NUnit.Framework;

namespace Tests.Core.UseCases.AddPlayerTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void BunchIdIsSet() => Assert.AreEqual(BunchId, Result.Slug);

        [Test]
        public void AddsPlayer() => Assert.IsNotNull(Added);
    }
}
