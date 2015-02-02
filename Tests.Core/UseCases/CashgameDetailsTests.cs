using System;
using Core.Entities;
using Core.Urls;
using Core.UseCases.CashgameDetails;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class CashgameDetailsTests : TestBase
    {
        [Test]
        public void CashgameDetails_AllBaseValuesAreSet()
        {
            var request = new CashgameDetailsRequest(Constants.SlugA, Constants.DateStringA);

            var result = Sut.Execute(request);

            Assert.AreEqual(Constants.DateStringA, result.Date.IsoString);
            Assert.AreEqual(Constants.LocationA, result.Location);
            Assert.AreEqual(62, result.Duration.Minutes);
            Assert.AreEqual(DateTime.Parse("2001-01-01 11:00:00"), result.StartTime);
            Assert.AreEqual(DateTime.Parse("2001-01-01 12:02:00"), result.EndTime);
            Assert.IsFalse(result.CanEdit);
            Assert.IsInstanceOf<EditCashgameUrl>(result.EditUrl);
            Assert.AreEqual(2, result.PlayerItems.Count);
        }

        [Test]
        public void CashgameDetails_WithResultsAndPlayers_PlayerResultItemsCountAndOrderIsCorrect()
        {
            var request = new CashgameDetailsRequest(Constants.SlugA, Constants.DateStringA);

            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(150, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(-150, result.PlayerItems[1].Winnings.Amount);
        }

        [Test]
        public void CashgameDetails_AllResultItemPropertiesAreSet()
        {
            var request = new CashgameDetailsRequest(Constants.SlugA, Constants.DateStringA);

            var result = Sut.Execute(request);

            Assert.AreEqual(Constants.PlayerNameB, result.PlayerItems[0].Name);
            Assert.AreEqual("/bunch-a/cashgame/action/2001-01-01/2", result.PlayerItems[0].PlayerUrl.Relative);
            Assert.AreEqual(200, result.PlayerItems[0].Buyin.Amount);
            Assert.AreEqual(350, result.PlayerItems[0].Cashout.Amount);
            Assert.AreEqual(150, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(148, result.PlayerItems[0].WinRate.Amount);
        }

        [Test]
        public void CashgameDetails_WithManager_CanEditIsTrue()
        {
            var request = new CashgameDetailsRequest(Constants.SlugA, Constants.DateStringA);

            Services.Auth.SetCurrentRole(Role.Manager);

            var result = Sut.Execute(request);

            Assert.IsTrue(result.CanEdit);
        }

        private CashgameDetailsInteractor Sut
        {
            get
            {
                return new CashgameDetailsInteractor(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Services.Auth,
                    Repos.Player);
            }
        }
    }
}