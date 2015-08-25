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
            var request = new RunningCashgame.Request(TestData.UserNameA, TestData.SlugA, DateTime.Now);

            Assert.Throws<CashgameNotRunningException>(() => Sut.Execute(request));
        }

        [Test]
        public void RunningCashgame_CashgameRunning_AllSimplePropertiesAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.UserNameA, TestData.SlugA, DateTime.Now);
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
        public void RunningCashgame_CashgameRunning_SlugIsSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.UserNameA, TestData.SlugA, DateTime.Now);
            var result = Sut.Execute(request);

            Assert.AreEqual("bunch-a", result.Slug);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_ItemsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.UserNameA, TestData.SlugA, TestData.StartTimeC);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(200, result.Items[0].Buyin.Amount);
            Assert.AreEqual(false, result.Items[0].HasCashedOut);
            Assert.AreEqual(TestData.PlayerA.DisplayName, result.Items[0].Name);
            Assert.AreEqual(3, result.Items[0].CashgameId);
            Assert.AreEqual(1, result.Items[0].PlayerId);
            Assert.AreEqual(200, result.Items[0].Stack.Amount);
            Assert.AreEqual("now", result.Items[0].Time.RelativeString);
            Assert.AreEqual(0, result.Items[0].Winnings.Amount);
            Assert.AreEqual(200, result.Items[1].Buyin.Amount);
            Assert.AreEqual(false, result.Items[1].HasCashedOut);
            Assert.AreEqual(TestData.PlayerB.DisplayName, result.Items[1].Name);
            Assert.AreEqual(3, result.Items[1].CashgameId);
            Assert.AreEqual(2, result.Items[1].PlayerId);
            Assert.AreEqual(200, result.Items[1].Stack.Amount);
            Assert.AreEqual("now", result.Items[1].Time.RelativeString);
            Assert.AreEqual(0, result.Items[1].Winnings.Amount);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_PlayerItemsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.UserNameA, TestData.SlugA, TestData.StartTimeC);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(1, result.PlayerItems[0].Checkpoints.Count);
            Assert.IsFalse(result.PlayerItems[0].HasCashedOut);
            Assert.AreEqual(TestData.PlayerA.DisplayName, result.PlayerItems[0].Name);
            Assert.AreEqual(TestData.PlayerA.Id, result.PlayerItems[0].PlayerId);
            Assert.AreEqual(3, result.PlayerItems[0].CashgameId);
            Assert.AreEqual(1, result.PlayerItems[0].PlayerId);
            Assert.AreEqual(1, result.PlayerItems[1].Checkpoints.Count);
            Assert.IsFalse(result.PlayerItems[1].HasCashedOut);
            Assert.AreEqual(TestData.PlayerB.DisplayName, result.PlayerItems[1].Name);
            Assert.AreEqual(TestData.PlayerB.Id, result.PlayerItems[1].PlayerId);
            Assert.AreEqual(3, result.PlayerItems[1].CashgameId);
            Assert.AreEqual(2, result.PlayerItems[1].PlayerId);
        }

        [Test]
        public void RunningCashgame_CashgameRunning_BunchPlayerItemsAreSet()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new RunningCashgame.Request(TestData.UserNameA, TestData.SlugA, TestData.StartTimeC);
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