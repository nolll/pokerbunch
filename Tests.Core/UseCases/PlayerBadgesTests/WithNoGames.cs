using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class WithNoGames : Arrange
    {
        protected override int NumberOfGames => 0;

        [Test]
        public void PlayerBadges_ZeroGames_AllBadgesAreFalse()
        {
            Assert.IsFalse(Result.PlayedOneGame);
            Assert.IsFalse(Result.PlayedTenGames);
            Assert.IsFalse(Result.Played50Games);
            Assert.IsFalse(Result.Played100Games);
            Assert.IsFalse(Result.Played200Games);
            Assert.IsFalse(Result.Played500Games);
        }
    }
}
