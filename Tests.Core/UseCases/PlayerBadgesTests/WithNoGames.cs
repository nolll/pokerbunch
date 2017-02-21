using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class WithNoGames : Arrange
    {
        protected override int NumberOfGames => 0;

        [Test]
        public void PlayerBadges_ZeroGames_AllBadgesAreFalse()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.PlayedOneGame);
            Assert.IsFalse(result.PlayedTenGames);
            Assert.IsFalse(result.Played50Games);
            Assert.IsFalse(result.Played100Games);
            Assert.IsFalse(result.Played200Games);
            Assert.IsFalse(result.Played500Games);
        }
    }
}
