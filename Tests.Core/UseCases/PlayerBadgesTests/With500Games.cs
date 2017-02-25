using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class With500Games : Arrange
    {
        protected override int NumberOfGames => 500;

        [Test]
        public void Played500GamesIsTrue() => Assert.IsTrue(Result.Played500Games);
    }
}