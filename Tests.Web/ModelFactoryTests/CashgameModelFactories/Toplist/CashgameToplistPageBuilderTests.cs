using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameTopList;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Toplist;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist
{
	public class CashgameToplistPageBuilderTests : MockContainer
    {
        [Test]
        public void Create_SetsTableModel()
        {
            const string slug = "a";
            var topListResult = new CashgameTopListResult();

            GetMock<ICashgameTopListInteractor>().Setup(o => o.Execute(It.IsAny<CashgameTopListRequest>())).Returns(topListResult);
            GetMock<ICashgameToplistTableModelFactory>().Setup(o => o.Create(topListResult)).Returns(new CashgameToplistTableModel());

            var sut = GetSut();
            var result = sut.Build(slug, null, null);

            Assert.IsInstanceOf<CashgameToplistTableModel>(result.TableModel);
        }

        private CashgameToplistPageBuilder GetSut()
        {
            return new CashgameToplistPageBuilder(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<ICashgameToplistTableModelFactory>().Object,
                GetMock<ICashgamePageNavigationModelFactory>().Object,
                GetMock<ICashgameYearNavigationModelFactory>().Object,
                GetMock<ICashgameTopListInteractor>().Object,
                GetMock<ICashgameContextInteractor>().Object);
        }
	}
}