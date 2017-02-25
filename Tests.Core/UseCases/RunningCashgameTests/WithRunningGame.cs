using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.RunningCashgameTests
{
    public class WithRunningGame : Arrange
    {
        [Test]
        public void RunningCashgame_CashgameRunning_AllSimplePropertiesAreSet()
        {
            var execute = Execute();
            Assert.AreEqual(PlayerData.Id1, execute.PlayerId);
            Assert.AreEqual(LocationData.Name1, execute.LocationName);
            Assert.AreEqual(100, execute.DefaultBuyin);
            Assert.IsFalse(execute.IsManager);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_SlugIsSet()
        {
            var result = Execute();
            Assert.AreEqual(BunchIdWithRunningGame, result.Slug);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_PlayerItemsAreSet()
        {
            var result = Execute();
            Assert.AreEqual(2, result.PlayerItems.Count);

            Assert.AreEqual(2, result.PlayerItems[0].Checkpoints.Count);
            Assert.IsTrue(result.PlayerItems[0].HasCashedOut);
            Assert.AreEqual(PlayerData.Name1, result.PlayerItems[0].Name);
            Assert.AreEqual(PlayerData.Id1, result.PlayerItems[0].PlayerId);
            Assert.AreEqual(CashgameData.Id1, result.PlayerItems[0].CashgameId);

            Assert.AreEqual(3, result.PlayerItems[1].Checkpoints.Count);
            Assert.IsTrue(result.PlayerItems[1].HasCashedOut);
            Assert.AreEqual(PlayerData.Name2, result.PlayerItems[1].Name);
            Assert.AreEqual(PlayerData.Id2, result.PlayerItems[1].PlayerId);
            Assert.AreEqual(CashgameData.Id1, result.PlayerItems[1].CashgameId);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_BunchPlayerItemsAreSet()
        {
            var result = Execute();
            Assert.AreEqual(2, result.BunchPlayerItems.Count);
            Assert.AreEqual(PlayerData.Name1, result.BunchPlayerItems[0].Name);
            Assert.AreEqual(PlayerData.Id1, result.BunchPlayerItems[0].PlayerId);
            Assert.AreEqual(PlayerData.Name2, result.BunchPlayerItems[1].Name);
            Assert.AreEqual(PlayerData.Id2, result.BunchPlayerItems[1].PlayerId);
        }
    }
}