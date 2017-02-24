using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerListTests
{
    public class WithPlayerRole : Arrange
    {
        [Test]
        public void BunchIdAndPlayersAreSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(BunchData.Id1, result.Slug);
            Assert.AreEqual(2, result.Players.Count);
            Assert.AreEqual(PlayerData.Id1, result.Players[0].Id);
            Assert.AreEqual(PlayerData.Name1, result.Players[0].Name);
            Assert.IsFalse(result.CanAddPlayer);
        }

        [Test]
        public void Execute_PlayersAreSortedAlphabetically()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(PlayerData.Name1, result.Players[0].Name);
            Assert.AreEqual(PlayerData.Name2, result.Players[1].Name);
        }
    }
}
