using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class With500Games : Arrange
    {
        protected override int NumberOfGames => 500;

        [Test]
        public void Played500GamesIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.Played500Games);
        }
    }
}