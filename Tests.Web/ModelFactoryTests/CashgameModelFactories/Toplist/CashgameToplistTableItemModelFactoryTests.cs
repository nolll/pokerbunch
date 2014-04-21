using Application.Services;
using Core.Classes;
using Core.UseCases.CashgameTopList;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Toplist;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist
{
	public class CashgameToplistTableItemModelFactoryTests : MockContainer
    {
		private Homegame _homegame;
	    private Player _player;
		private int _rank;
	    private string _slug;
	    private TopListItem _topListItem;
	    private CurrencySettings _currency;

	    [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_player = new FakePlayer(displayName: "player name");
			_rank = 1;
	        _slug = "a";
            _topListItem = new TopListItem();
            _currency = new CurrencySettings("", "");
		}

        [Test]
		public void TableItem_RankIsSet()
        {
            _topListItem.Rank = 1;
            
            var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, _currency, ToplistSortOrder.Winnings);

			Assert.AreEqual(1, result.Rank);
		}

		[Test]
		public void TableItem_PlayerNameIsSet()
		{
		    _topListItem.Name = "player name";
            
            var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, _currency, ToplistSortOrder.Winnings);

			Assert.AreEqual("player name", result.Name);
			Assert.AreEqual("player%20name", result.UrlEncodedName);
		}

		[Test]
		public void TableItem_TotalResultIsSet()
		{
		    const string formattedResult = "a";
		    const int winnings = 1;
            _topListItem.Winnings = winnings;

            GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), winnings)).Returns(formattedResult);

			var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, _currency, ToplistSortOrder.Winnings);

			Assert.AreEqual(formattedResult, result.TotalResult);
		}

		[Test]
		public void TableItem_WinningsClassIsSet()
        {
            const string resultClass = "a";
            const int winnings = 1;
            _topListItem.Winnings = winnings;

            GetMock<IResultFormatter>().Setup(o => o.GetWinningsCssClass(winnings)).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, _currency, ToplistSortOrder.Winnings);

			Assert.AreEqual(resultClass, result.ResultClass);
		}

		[Test]
		public void TableItem_WithDuration_DurationIsSet()
		{
		    const string formattedTime = "a";
		    const int timePlayed = 60;
		    _topListItem.MinutesPlayed = timePlayed;

            GetMock<IGlobalization>().Setup(o => o.FormatDuration(timePlayed)).Returns(formattedTime);

			var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, _currency, ToplistSortOrder.Winnings);

            Assert.AreEqual(formattedTime, result.GameTime);
		}

		[Test]
		public void TableItem_WithDuration_WinrateIsSet()
		{
		    const string formattedWinRate = "a";
            const int winRate = 1;
		    _topListItem.WinRate = 1;

            GetMock<IGlobalization>().Setup(o => o.FormatWinrate(_currency, winRate)).Returns(formattedWinRate);

			var sut = GetSut();
            var result = sut.Create(_topListItem, _slug, _currency, ToplistSortOrder.Winnings);

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
            var result = sut.Create(_topListItem, _slug, _currency, ToplistSortOrder.Winnings);

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