using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class With100Games : Arrange
    {
        protected override int NumberOfGames => 100;

        [Test]
        public void Played100GamesIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.Played100Games);
        }
    }
}