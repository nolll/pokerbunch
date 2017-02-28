using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.RunningCashgameTests
{
    public class WithRunningGame : Arrange
    {
        [Test]
        public void RunningCashgame_CashgameRunning_AllSimplePropertiesAreSet()
        {
            Assert.AreEqual(PlayerData.Id1, Result.PlayerId);
            Assert.AreEqual(LocationData.Name1, Result.LocationName);
            Assert.AreEqual(100, Result.DefaultBuyin);
            Assert.IsFalse(Result.IsManager);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_SlugIsSet()
        {
            Assert.AreEqual(BunchIdWithRunningGame, Result.Slug);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_PlayerItemsAreSet()
        {
            Assert.AreEqual(2, Result.PlayerItems.Count);
            Assert.AreEqual(2, Result.PlayerItems[0].Checkpoints.Count);
            Assert.IsTrue(Result.PlayerItems[0].HasCashedOut);
            Assert.AreEqual(PlayerData.Name1, Result.PlayerItems[0].Name);
            Assert.AreEqual(PlayerData.Id1, Result.PlayerItems[0].PlayerId);
            Assert.AreEqual(CashgameData.Id1, Result.PlayerItems[0].CashgameId);

            Assert.AreEqual(3, Result.PlayerItems[1].Checkpoints.Count);
            Assert.IsTrue(Result.PlayerItems[1].HasCashedOut);
            Assert.AreEqual(PlayerData.Name2, Result.PlayerItems[1].Name);
            Assert.AreEqual(PlayerData.Id2, Result.PlayerItems[1].PlayerId);
            Assert.AreEqual(CashgameData.Id1, Result.PlayerItems[1].CashgameId);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_BunchPlayerItemsAreSet()
        {
            Assert.AreEqual(2, Result.BunchPlayerItems.Count);
            Assert.AreEqual(PlayerData.Name1, Result.BunchPlayerItems[0].Name);
            Assert.AreEqual(PlayerData.Id1, Result.BunchPlayerItems[0].PlayerId);
            Assert.AreEqual(PlayerData.Name2, Result.BunchPlayerItems[1].Name);
            Assert.AreEqual(PlayerData.Id2, Result.BunchPlayerItems[1].PlayerId);
        }
    }
}