using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerDetailsTests
{
    public class WithPlayerThatHasNotPlayedGamesAndUserIsManager : Arrange
    {
        protected override Role Role => Role.Manager;
        protected override string PlayerId => IdForPlayerThatHasNotPlayedGames;

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasNotPlayedGames_CanDeleteIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.CanDelete);
        }
    }
}