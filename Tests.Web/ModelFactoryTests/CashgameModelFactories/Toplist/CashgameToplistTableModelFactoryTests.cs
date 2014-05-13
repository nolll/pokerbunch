using System.Collections.Generic;
using Application.UseCases.CashgameTopList;
using NUnit.Framework;
using Tests.Common;
using Web.Models.CashgameModels.Toplist;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist
{
	public class CashgameToplistTableModelFactoryTests : MockContainer
    {
        [Test]
	    public void Table_ItemModelsAreSet()
	    {
            var items = new List<TopListItem>();
            var topListResult = new TopListResult{Items = items};

            var result = new ToplistTableModel(topListResult);

            Assert.AreEqual(items, result.ItemModels);
	    }

	    [Test]
	    public void SortUrls_AllUrlsAreCorrect()
	    {
            var items = new List<TopListItem>();
            var topListResult = new TopListResult { Items = items };
            
	        var result = new ToplistTableModel(topListResult);

            StringAssert.EndsWith("?orderby=winnings", result.ColumnsModel.ResultColumn.Url);
            StringAssert.EndsWith("?orderby=buyin", result.ColumnsModel.BuyinColumn.Url);
            StringAssert.EndsWith("?orderby=cashout", result.ColumnsModel.CashoutColumn.Url);
            StringAssert.EndsWith("?orderby=timeplayed", result.ColumnsModel.TimePlayedColumn.Url);
            StringAssert.EndsWith("?orderby=gamesplayed", result.ColumnsModel.GamesPlayedColumn.Url);
            StringAssert.EndsWith("?orderby=winrate", result.ColumnsModel.WinRateColumn.Url);
	    }
	}
}