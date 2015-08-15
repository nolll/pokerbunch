using System;
using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class RunningCashgameTests : TestBase
    {
        [Test]
        public void RunningCashgame_CashgameNotRunning_ThrowsException()
        {
            var request = new RunningCashgame.Request(TestData.SlugA, TestData.UserNameA, DateTime.Now);

            Assert.Throws<CashgameNotRunningException>(() => Sut.Execute(request));
        }

        [Test]
        public void RunningCashgame_CashgameRunning_AllSimplePropertiesAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.SlugA, TestData.UserNameA, DateTime.Now);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.PlayerIdA, result.PlayerId);
            Assert.AreEqual(TestData.LocationC, result.Location);
            Assert.IsTrue(result.ShowStartTime);
            Assert.AreEqual("11:00", result.StartTime);
            Assert.IsTrue(result.IsStarted);
            Assert.IsTrue(result.ShowTable);
            Assert.IsTrue(result.ShowChart);
            Assert.AreEqual(400, result.TotalBuyin.Amount);
            Assert.AreEqual(400, result.TotalStacks.Amount);
            Assert.AreEqual(100, result.DefaultBuyin);
            Assert.IsFalse(result.IsManager);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_AllUrlsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.SlugA, TestData.UserNameA, DateTime.Now);
            var result = Sut.Execute(request);

            Assert.AreEqual("/cashgame/runningplayersjson/bunch-a", result.PlayersDataUrl.Relative);
            Assert.AreEqual("/cashgame/runninggamejson/bunch-a", result.GameDataUrl.Relative);
            Assert.AreEqual("/cashgame/buyin/bunch-a", result.BuyinUrl.Relative);
            Assert.AreEqual("/cashgame/report/bunch-a", result.ReportUrl.Relative);
            Assert.AreEqual("/cashgame/cashout/bunch-a", result.CashoutUrl.Relative);
            Assert.AreEqual("/cashgame/end/bunch-a", result.EndGameUrl.Relative);
            Assert.AreEqual("/cashgame/index/bunch-a", result.CashgameIndexUrl.Relative);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_ItemsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.SlugA, TestData.UserNameA, TestData.StartTimeC);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(200, result.Items[0].Buyin.Amount);
            Assert.AreEqual(false, result.Items[0].HasCashedOut);
            Assert.AreEqual(TestData.PlayerA.DisplayName, result.Items[0].Name);
            Assert.AreEqual("/cashgame/action/bunch-a/2003-03-03/1", result.Items[0].PlayerUrl.Relative);
            Assert.AreEqual(200, result.Items[0].Stack.Amount);
            Assert.AreEqual("now", result.Items[0].Time.RelativeString);
            Assert.AreEqual(0, result.Items[0].Winnings.Amount);
            Assert.AreEqual(200, result.Items[1].Buyin.Amount);
            Assert.AreEqual(false, result.Items[1].HasCashedOut);
            Assert.AreEqual(TestData.PlayerB.DisplayName, result.Items[1].Name);
            Assert.AreEqual("/cashgame/action/bunch-a/2003-03-03/2", result.Items[1].PlayerUrl.Relative);
            Assert.AreEqual(200, result.Items[1].Stack.Amount);
            Assert.AreEqual("now", result.Items[1].Time.RelativeString);
            Assert.AreEqual(0, result.Items[1].Winnings.Amount);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_PlayerItemsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.SlugA, TestData.UserNameA, TestData.StartTimeC);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(1, result.PlayerItems[0].Checkpoints.Count);
            Assert.IsFalse(result.PlayerItems[0].HasCashedOut);
            Assert.AreEqual(TestData.PlayerA.DisplayName, result.PlayerItems[0].Name);
            Assert.AreEqual(TestData.PlayerA.Id, result.PlayerItems[0].PlayerId);
            Assert.AreEqual("/cashgame/action/bunch-a/2003-03-03/1", result.PlayerItems[0].PlayerUrl.Relative);
            Assert.AreEqual(1, result.PlayerItems[1].Checkpoints.Count);
            Assert.IsFalse(result.PlayerItems[1].HasCashedOut);
            Assert.AreEqual(TestData.PlayerB.DisplayName, result.PlayerItems[1].Name);
            Assert.AreEqual(TestData.PlayerB.Id, result.PlayerItems[1].PlayerId);
            Assert.AreEqual("/cashgame/action/bunch-a/2003-03-03/1", result.PlayerItems[0].PlayerUrl.Relative);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_BunchPlayerItemsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.SlugA, TestData.UserNameA, TestData.StartTimeC);
            var result = Sut.Execute(request);

            Assert.AreEqual(4, result.BunchPlayerItems.Count);
            Assert.AreEqual(TestData.PlayerA.DisplayName, result.BunchPlayerItems[0].Name);
            Assert.AreEqual(TestData.PlayerA.Id, result.BunchPlayerItems[0].PlayerId);
            Assert.AreEqual(TestData.PlayerB.DisplayName, result.BunchPlayerItems[1].Name);
            Assert.AreEqual(TestData.PlayerB.Id, result.BunchPlayerItems[1].PlayerId);
            Assert.AreEqual(TestData.PlayerC.DisplayName, result.BunchPlayerItems[2].Name);
            Assert.AreEqual(TestData.PlayerC.Id, result.BunchPlayerItems[2].PlayerId);
            Assert.AreEqual(TestData.PlayerD.DisplayName, result.BunchPlayerItems[3].Name);
            Assert.AreEqual(TestData.PlayerD.Id, result.BunchPlayerItems[3].PlayerId);
        }

        private RunningCashgame Sut
        {
            get
            {
                return new RunningCashgame(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Player,
                    Repos.User);
            }
        }
    }
}