using System;
using Core.Exceptions;
using Core.UseCases.RunningCashgame;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class RunningCashgameTests : TestBase
    {
        [Test]
        public void RunningCashgame_CashgameNotRunning_ThrowsException()
        {
            var request = new RunningCashgameRequest(Constants.SlugA, Constants.UserIdA, DateTime.Now);

            Assert.Throws<CashgameNotRunningException>(() => Sut.Execute(request));
        }

        [Test]
        public void RunningCashgame_CashgameRunning_AllSimplePropertiesAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgameRequest(Constants.SlugA, Constants.UserIdA, DateTime.Now);
            var result = Sut.Execute(request);

            Assert.AreEqual(Constants.PlayerIdA, result.PlayerId);
            Assert.AreEqual(Constants.LocationC, result.Location);
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

            var request = new RunningCashgameRequest(Constants.SlugA, Constants.UserIdA, DateTime.Now);
            var result = Sut.Execute(request);

            Assert.AreEqual("/bunch-a/cashgame/runningplayersjson", result.PlayersDataUrl.Relative);
            Assert.AreEqual("/bunch-a/cashgame/runninggamejson", result.GameDataUrl.Relative);
            Assert.AreEqual("/bunch-a/cashgame/buyin", result.BuyinUrl.Relative);
            Assert.AreEqual("/bunch-a/cashgame/report", result.ReportUrl.Relative);
            Assert.AreEqual("/bunch-a/cashgame/cashout", result.CashoutUrl.Relative);
            Assert.AreEqual("/bunch-a/cashgame/end", result.EndGameUrl.Relative);
            Assert.AreEqual("/bunch-a/cashgame", result.CashgameIndexUrl.Relative);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_ItemsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgameRequest(Constants.SlugA, Constants.UserIdA, Constants.StartTimeC);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(200, result.Items[0].Buyin.Amount);
            Assert.AreEqual(false, result.Items[0].HasCashedOut);
            Assert.AreEqual(Constants.PlayerNameA, result.Items[0].Name);
            Assert.AreEqual("/bunch-a/cashgame/action/2003-03-03/1", result.Items[0].PlayerUrl.Relative);
            Assert.AreEqual(200, result.Items[0].Stack.Amount);
            Assert.AreEqual("now", result.Items[0].Time.RelativeString);
            Assert.AreEqual(0, result.Items[0].Winnings.Amount);
            Assert.AreEqual(200, result.Items[1].Buyin.Amount);
            Assert.AreEqual(false, result.Items[1].HasCashedOut);
            Assert.AreEqual(Constants.PlayerNameB, result.Items[1].Name);
            Assert.AreEqual("/bunch-a/cashgame/action/2003-03-03/2", result.Items[1].PlayerUrl.Relative);
            Assert.AreEqual(200, result.Items[1].Stack.Amount);
            Assert.AreEqual("now", result.Items[1].Time.RelativeString);
            Assert.AreEqual(0, result.Items[1].Winnings.Amount);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_PlayerItemsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgameRequest(Constants.SlugA, Constants.UserIdA, Constants.StartTimeC);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(1, result.PlayerItems[0].Checkpoints.Count);
            Assert.IsFalse(result.PlayerItems[0].HasCashedOut);
            Assert.AreEqual(Constants.PlayerNameA, result.PlayerItems[0].Name);
            Assert.AreEqual(Constants.PlayerIdA, result.PlayerItems[0].PlayerId);
            Assert.AreEqual(1, result.PlayerItems[1].Checkpoints.Count);
            Assert.IsFalse(result.PlayerItems[1].HasCashedOut);
            Assert.AreEqual(Constants.PlayerNameB, result.PlayerItems[1].Name);
            Assert.AreEqual(Constants.PlayerIdB, result.PlayerItems[1].PlayerId);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_BunchPlayerItemsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgameRequest(Constants.SlugA, Constants.UserIdA, Constants.StartTimeC);
            var result = Sut.Execute(request);

            Assert.AreEqual(4, result.BunchPlayerItems.Count);
            Assert.AreEqual(Constants.PlayerNameA, result.BunchPlayerItems[0].Name);
            Assert.AreEqual(Constants.PlayerIdA, result.BunchPlayerItems[0].PlayerId);
            Assert.AreEqual(Constants.PlayerNameB, result.BunchPlayerItems[1].Name);
            Assert.AreEqual(Constants.PlayerIdB, result.BunchPlayerItems[1].PlayerId);
            Assert.AreEqual(Constants.PlayerNameC, result.BunchPlayerItems[2].Name);
            Assert.AreEqual(Constants.PlayerIdC, result.BunchPlayerItems[2].PlayerId);
            Assert.AreEqual(Constants.PlayerNameD, result.BunchPlayerItems[3].Name);
            Assert.AreEqual(Constants.PlayerIdD, result.BunchPlayerItems[3].PlayerId);
        }

        private RunningCashgameInteractor Sut
        {
            get
            {
                return new RunningCashgameInteractor(
                    Services.Auth,
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Player);
            }
        }
    }
}