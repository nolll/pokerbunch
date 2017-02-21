using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public class WithOneGame : Arrange
    {
        protected override int NumberOfGames => 1;

        [Test]
        public void PlayedOneGameIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.PlayedOneGame);
        }
    }
}