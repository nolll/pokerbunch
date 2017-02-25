using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerDetailsTests
{
    public class WithPlayerThatHasPlayedGamesAndUserIsManager : Arrange
    {
        protected override Role Role => Role.Manager;
        protected override string PlayerId => IdForPlayerThatHasPlayedGames;

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasPlayedGames_CanDeleteIsFalse()
        {
            Assert.IsFalse(Result.CanDelete);
        }
    }
}