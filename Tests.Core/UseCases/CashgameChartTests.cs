using Core.UseCases.CashgameChart;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class CashgameChartTests : TestBase
    {
        [Test]
        public void CashgameChart_GameDataIsCorrect()
        {
            var request = new CashgameChartRequest(Constants.SlugA, null);
            var result = Execute(request);

            Assert.AreEqual(2, result.GameItems.Count);

            Assert.AreEqual("2001-01-01", result.GameItems[0].Date.IsoString);
            Assert.AreEqual(2, result.GameItems[0].Winnings.Count);
            Assert.AreEqual(-150, result.GameItems[0].Winnings[Constants.PlayerIdA]);
            Assert.AreEqual(150, result.GameItems[0].Winnings[Constants.PlayerIdB]);
            Assert.AreEqual(2, result.GameItems[1].Winnings.Count);
            Assert.AreEqual(200, result.GameItems[1].Winnings[Constants.PlayerIdA]);
            Assert.AreEqual(-200, result.GameItems[1].Winnings[Constants.PlayerIdB]);
        }

        [Test]
        public void CashgameChart_PlayerDataIsCorrect()
        {
            var request = new CashgameChartRequest(Constants.SlugA, null);
            var result = Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(Constants.PlayerIdA, result.PlayerItems[0].Id);
            Assert.AreEqual(Constants.PlayerNameA, result.PlayerItems[0].Name);
            Assert.AreEqual(Constants.PlayerIdB, result.PlayerItems[1].Id);
            Assert.AreEqual(Constants.PlayerNameB, result.PlayerItems[1].Name);
        }

        private CashgameChartResult Execute(CashgameChartRequest request)
        {
            return CashgameChartInteractor.Execute(
                Repos.Bunch,
                Repos.Cashgame,
                Repos.Player,
                request);
        }
    }
}
