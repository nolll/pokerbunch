using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class With50Games : Arrange
    {
        protected override int NumberOfGames => 50;

        [Test]
        public void Played50GamesIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.Played50Games);
        }
    }
}