using System;
using Core.UseCases.CashgameDetailsChart;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class CashgameDetailsChartTests : TestBase
    {
        [Test]
        public void PlayerItems_EndedGame_TwoPlayersWithTwoAndThreeCheckpoints()
        {
            var request = new CashgameDetailsChartRequest(TestData.SlugA, DateTime.Now, TestData.DateStringA);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(TestData.PlayerNameA, result.PlayerItems[0].Name);
            Assert.AreEqual(2, result.PlayerItems[0].Results.Count);
            Assert.AreEqual(0, result.PlayerItems[0].Results[0].Winnings);
            Assert.AreEqual(-150, result.PlayerItems[0].Results[1].Winnings);
            Assert.AreEqual(3, result.PlayerItems[1].Results.Count);
            Assert.AreEqual(0, result.PlayerItems[1].Results[0].Winnings);
            Assert.AreEqual(50, result.PlayerItems[1].Results[1].Winnings);
            Assert.AreEqual(150, result.PlayerItems[1].Results[2].Winnings);
        }

        [Test]
        public void PlayerItems_RunningGame_TwoPlayersWithTwoCheckpointsEach()
        {
            Repos.Cashgame.SetupRunningGame();

            var request = new CashgameDetailsChartRequest(TestData.SlugA, DateTime.Now);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(TestData.PlayerNameA, result.PlayerItems[0].Name);
            Assert.AreEqual(2, result.PlayerItems[0].Results.Count);
            Assert.AreEqual(0, result.PlayerItems[0].Results[0].Winnings);
            Assert.AreEqual(0, result.PlayerItems[0].Results[1].Winnings);
            Assert.AreEqual(2, result.PlayerItems[1].Results.Count);
            Assert.AreEqual(0, result.PlayerItems[1].Results[0].Winnings);
            Assert.AreEqual(0, result.PlayerItems[1].Results[1].Winnings);
        }

        public CashgameDetailsChartInteractor Sut
        {
            get
            {
                return new CashgameDetailsChartInteractor(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Player);
            }
        }
    }
}