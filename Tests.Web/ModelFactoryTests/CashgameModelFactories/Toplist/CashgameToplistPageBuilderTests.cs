using System.Collections.Generic;
using Application.Services;
using Core.Repositories;
using Core.Services.Interfaces;
using Core.UseCases.CashgameTopList;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Toplist;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist{

	public class CashgameToplistPageBuilderTests : MockContainer {

        [Test]
        public void Create_SetsTableModelOld()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var suite = new FakeCashgameSuite();
            const int year = 1;

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameService>().Setup(o => o.GetSuite(homegame, year)).Returns(suite);
            GetMock<ICashgameToplistTableModelFactory>().Setup(o => o.Create(homegame, suite, year, ToplistSortOrder.Winnings)).Returns(new CashgameToplistTableModel());

            var sut = GetSut();
            var result = sut.Build(slug, null, year);

            Assert.IsInstanceOf<CashgameToplistTableModel>(result.TableModel);
        }

        /*
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
        */

        private CashgameToplistPageBuilder GetSut()
        {
            return new CashgameToplistPageBuilder(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<ICashgameToplistTableModelFactory>().Object,
                GetMock<ICashgamePageNavigationModelFactory>().Object,
                GetMock<ICashgameYearNavigationModelFactory>().Object,
                GetMock<IHomegameRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<ICashgameService>().Object,
                GetMock<ICashgameTopListInteractor>().Object);
        }

	}

}