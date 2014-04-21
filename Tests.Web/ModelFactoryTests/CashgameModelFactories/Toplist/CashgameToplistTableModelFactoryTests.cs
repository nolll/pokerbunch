using System.Collections.Generic;
using Application.Services;
using Core.UseCases.CashgameTopList;
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
	        var topListItem = new TopListItem();
	        var topListItems = new List<TopListItem> {topListItem, topListItem};
            var topListResult = new CashgameTopListResult{Items = topListItems};

	        var sut = GetSut();
	        var result = sut.Create(topListResult);

            Assert.AreEqual(2, result.ItemModels.Count);
	    }

	    private CashgameToplistTableModelFactory GetSut()
        {
			return new CashgameToplistTableModelFactory(
                GetMock<ICashgameToplistTableItemModelFactory>().Object,
                GetMock<IUrlProvider>().Object);
		}

	}

}