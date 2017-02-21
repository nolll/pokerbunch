using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class With10Games : Arrange
    {
        protected override int NumberOfGames => 10;

        [Test]
        public void PlayedTenGamesIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.PlayedTenGames);
        }
    }
}