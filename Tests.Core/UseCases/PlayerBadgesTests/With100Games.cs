using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class With100Games : Arrange
    {
        protected override int NumberOfGames => 100;

        [Test]
        public void Played100GamesIsTrue() => Assert.IsTrue(Result.Played100Games);
    }
}