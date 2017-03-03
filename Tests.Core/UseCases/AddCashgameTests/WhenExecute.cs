using NUnit.Framework;

namespace Tests.Core.UseCases.AddCashgameTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void SlugIsSet() => Assert.AreEqual(BunchId, Result.Slug);

        [Test]
        public void GameIsAdded()
        {
            Assert.IsNotNull(AddedCashgame);
        }
    }
}
