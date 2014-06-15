using Application.Urls;
using Application.UseCases.CashgameTopList;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Models.CashgameModels.Toplist;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist
{
	public class CashgameToplistTableItemModelFactoryTests : MockContainer
    {
	    private string _slug;
	    private TopListItem _topListItem;

	    [SetUp]
		public void SetUp(){
	        _slug = "a";
            _topListItem = new TopListItem
                {
                    Buyin = new MoneyInTest(),
                    Cashout = new MoneyInTest(),
                    Winnings = new MoneyInTest(),
                    WinRate = new MoneyInTest(),
                    TimePlayed = new TimeInTest()
                };
		}

        [Test]
		public void TableItem_RankIsSet()
        {
            _topListItem.Rank = 1;

            var result = new CashgameToplistTableItemModel(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual(1, result.Rank);
		}

		[Test]
		public void TableItem_PlayerNameIsSet()
		{
		    _topListItem.Name = "player name";
            
            var result = new CashgameToplistTableItemModel(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual("player name", result.Name);
			Assert.AreEqual("player%20name", result.UrlEncodedName);
		}

		[Test]
		public void TableItem_TotalResultIsSet()
		{
		    const string formattedResult = "1";
            var winnings = new MoneyInTest(1);
            _topListItem.Winnings = winnings;

            var result = new CashgameToplistTableItemModel(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual(formattedResult, result.TotalResult);
		}

		[Test]
		public void TableItem_WinningsClassIsSet()
        {
            const string resultClass = "pos-result";
            var winnings = new MoneyInTest(1); 
            _topListItem.Winnings = winnings;

            var result = new CashgameToplistTableItemModel(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual(resultClass, result.ResultClass);
		}

		[Test]
		public void TableItem_WithDuration_DurationIsSet()
		{
		    const string formattedTime = "0";
            var timePlayed = new TimeInTest();
		    _topListItem.TimePlayed = timePlayed;

            var result = new CashgameToplistTableItemModel(_topListItem, _slug, ToplistSortOrder.Winnings);

            Assert.AreEqual(formattedTime, result.GameTime);
		}

		[Test]
		public void TableItem_WithDuration_WinrateIsSet()
		{
		    const string formattedWinRate = "1";
            var winRate = new MoneyInTest(1);
		    _topListItem.WinRate = winRate;

            var result = new CashgameToplistTableItemModel(_topListItem, _slug, ToplistSortOrder.Winnings);

			Assert.AreEqual(formattedWinRate, result.WinRate);
		}

		[Test]
		public void TableItem_PlayerUrlIsSet()
		{
		    const string playerName = "b";
            _topListItem.Name = playerName;

            var result = new CashgameToplistTableItemModel(_topListItem, _slug, ToplistSortOrder.Winnings);

            Assert.IsInstanceOf<PlayerDetailsUrl>(result.PlayerUrl);
		}

	}
}