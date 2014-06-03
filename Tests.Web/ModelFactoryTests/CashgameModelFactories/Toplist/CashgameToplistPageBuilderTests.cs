using System.Collections.Generic;
using Application.UseCases.ApplicationContext;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.Models.CashgameModels.Toplist;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist
{
	public class CashgameToplistPageBuilderTests : MockContainer
    {
        [Test]
        public void Create_SetsTableModel()
        {
            const string slug = "a";
            var topListResult = new TopListResult{Items = new List<TopListItem>()};
            var cashgameContextResult = new CashgameContextResultInTest();
            var applicationContextResult = new ApplicationContextResultInTest();

            GetMock<ITopListInteractor>().Setup(o => o.Execute(It.IsAny<TopListRequest>())).Returns(topListResult);
            GetMock<ICashgameContextInteractor>().Setup(o => o.Execute(It.IsAny<CashgameContextRequest>())).Returns(cashgameContextResult);
            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(applicationContextResult);

            var sut = GetSut();
            var result = sut.Build(slug, null, null);

            Assert.IsInstanceOf<ToplistTableModel>(result.TableModel);
        }

        private ToplistPageBuilder GetSut()
        {
            return new ToplistPageBuilder(
                GetMock<ITopListInteractor>().Object,
                GetMock<ICashgameContextInteractor>().Object);
        }
	}
}