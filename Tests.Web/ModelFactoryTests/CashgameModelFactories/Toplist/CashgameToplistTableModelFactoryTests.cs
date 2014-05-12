using System.Collections.Generic;
using Application.Services;
using Application.UseCases.CashgameTopList;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Toplist;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist
{
	public class CashgameToplistTableModelFactoryTests : MockContainer
    {
        [Test]
	    public void Table_ItemModelsAreSet()
	    {
            var items = new List<TopListItem>();
            var topListResult = new CashgameTopListResult{Items = items};
            
	        var sut = GetSut();
	        var result = sut.Create(topListResult);

            Assert.AreEqual(items, result.ItemModels);
	    }

	    [Test]
	    public void SortUrls_AllUrlsAreCorrect()
	    {
            var items = new List<TopListItem>();
            var topListResult = new CashgameTopListResult { Items = items };
            
	        var sut = GetSut();
	        var result = sut.Create(topListResult);

            Assert.AreEqual("?orderby=winnings", result.ResultSortUrl);
            Assert.AreEqual("?orderby=buyin", result.BuyinSortUrl);
            Assert.AreEqual("?orderby=cashout", result.CashoutSortUrl);
            Assert.AreEqual("?orderby=timeplayed", result.GameTimeSortUrl);
            Assert.AreEqual("?orderby=gamesplayed", result.GameCountSortUrl);
            Assert.AreEqual("?orderby=winrate", result.WinRateSortUrl);
	    }

	    private CashgameToplistTableModelFactory GetSut()
        {
			return new CashgameToplistTableModelFactory(
                GetMock<ICashgameToplistTableItemModelFactory>().Object,
                GetMock<IUrlProvider>().Object);
		}
	}
}