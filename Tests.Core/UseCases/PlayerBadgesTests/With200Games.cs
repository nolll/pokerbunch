using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class With200Games : Arrange
    {
        protected override int NumberOfGames => 200;

        [Test]
        public void Played200GamesIsTrue() => Assert.IsTrue(Result.Played200Games);
    }
}