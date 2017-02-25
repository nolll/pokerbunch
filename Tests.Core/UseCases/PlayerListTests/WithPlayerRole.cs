using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerListTests
{
    public class WithPlayerRole : Arrange
    {
        [Test]
        public void BunchIdAndPlayersAreSet()
        {
            Assert.AreEqual(BunchData.Id1, Result.Slug);
            Assert.AreEqual(2, Result.Players.Count);
            Assert.AreEqual(PlayerData.Id1, Result.Players[0].Id);
            Assert.AreEqual(PlayerData.Name1, Result.Players[0].Name);
            Assert.IsFalse(Result.CanAddPlayer);
        }

        [Test]
        public void Execute_PlayersAreSortedAlphabetically()
        {
            Assert.AreEqual(PlayerData.Name1, Result.Players[0].Name);
            Assert.AreEqual(PlayerData.Name2, Result.Players[1].Name);
        }
    }
}
