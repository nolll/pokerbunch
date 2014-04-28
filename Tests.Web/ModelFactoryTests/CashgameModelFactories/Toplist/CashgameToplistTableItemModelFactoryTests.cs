using System;
using Application.Services;
using Application.UseCases.CashgameFacts;
using Application.UseCases.CashgameTopList;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Toplist;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist
{
	public class CashgameToplistTableItemModelFactoryTests : MockContainer
    {
	    private string _slug;
	    private TopListItem _topListItem;

	    [SetUp]
		public void SetUp(){
	        _slug = "a";
            _topListItem = new TopListItem();
		}

        [Test]
		public void TableItem_RankIsSet()
        {
            _topListItem.Rank = 1;
            
            var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual(1, result.Rank);
		}

		[Test]
		public void TableItem_PlayerNameIsSet()
		{
		    _topListItem.Name = "player name";
            
            var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual("player name", result.Name);
			Assert.AreEqual("player%20name", result.UrlEncodedName);
		}

		[Test]
		public void TableItem_TotalResultIsSet()
		{
		    const string formattedResult = "a";
            var winnings = new Money(1);
            _topListItem.Winnings = winnings;

            GetMock<IGlobalization>().Setup(o => o.FormatResult(winnings)).Returns(formattedResult);

			var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual(formattedResult, result.TotalResult);
		}

		[Test]
		public void TableItem_WinningsClassIsSet()
        {
            const string resultClass = "a";
            var winnings = new Money(1); 
            _topListItem.Winnings = winnings;

            GetMock<IResultFormatter>().Setup(o => o.GetWinningsCssClass(winnings)).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual(resultClass, result.ResultClass);
		}

		[Test]
		public void TableItem_WithDuration_DurationIsSet()
		{
		    const string formattedTime = "a";
            var timePlayed = TimeSpan.FromMinutes(60);
		    _topListItem.MinutesPlayed = timePlayed;

            GetMock<IGlobalization>().Setup(o => o.FormatDuration(timePlayed)).Returns(formattedTime);

			var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, ToplistSortOrder.Winnings);

            Assert.AreEqual(formattedTime, result.GameTime);
		}

		[Test]
		public void TableItem_WithDuration_WinrateIsSet()
		{
		    const string formattedWinRate = "a";
            var winRate = new Money(1);
		    _topListItem.WinRate = winRate;

            GetMock<IGlobalization>().Setup(o => o.FormatWinrate(winRate)).Returns(formattedWinRate);

			var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual(formattedWinRate, result.WinRate);
		}

		[Test]
		public void TableItem_PlayerUrlIsSet()
		{
		    const string playerUrl = "a";
		    const string playerName = "b";
            _topListItem.Name = playerName;

		    GetMock<IUrlProvider>().Setup(o => o.GetPlayerDetailsUrl(_slug, playerName)).Returns(playerUrl);

			var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, ToplistSortOrder.Winnings);

            Assert.AreEqual(playerUrl, result.PlayerUrl);
		}

		private CashgameToplistTableItemModelFactory GetSut()
        {
			return new CashgameToplistTableItemModelFactory(
                GetMock<IUrlProvider>().Object,
                GetMock<IResultFormatter>().Object,
                GetMock<IGlobalization>().Object);
		}
	}
}