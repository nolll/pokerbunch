using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Add;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.CashgameModelFactories.Cashout;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Checkpoints;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.End;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.ModelServices;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Toplist;
using Web.Models.CashgameModels.Matrix;
using Web.Security;

namespace Tests.Web.ModelServiceTests
{
    public class CashgameModelServiceTests : MockContainer
    {
        [Test]
        public void GetIndexUrl_CashgameServiceReturnsYear_ReturnsMatrixUrl()
        {
            const string slug = "a";
            const string url = "b";
            const int year = 1;

            GetMock<ICashgameService>().Setup(o => o.GetLatestYear(slug)).Returns(year);
            GetMock<IUrlProvider>().Setup(o => o.GetCashgameMatrixUrl(slug, year)).Returns(url);

            var sut = GetSut();
            var result = sut.GetIndexUrl(slug);

            Assert.AreEqual(url, result);
        }

        [Test]
        public void GetIndexUrl_CashgameServiceReturnsNoYear_ReturnsAddCashgameUrl()
        {
            const string slug = "a";
            const string url = "b";

            GetMock<IUrlProvider>().Setup(o => o.GetCashgameAddUrl(slug)).Returns(url);

            var sut = GetSut();
            var result = sut.GetIndexUrl(slug);

            Assert.AreEqual(url, result);
        }

        [Test]
        public void GetMatrixModel_Authorized_ReturnsModel()
        {
            const string slug = "a";
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(new FakeUser());
            GetMock<IMatrixPageModelFactory>().Setup(o => o.Create(It.IsAny<Homegame>(), It.IsAny<User>(), It.IsAny<int?>())).Returns(new CashgameMatrixPageModel());

            var sut = GetSut();
            var result = sut.GetMatrixModel(slug);

            Assert.IsNotNull(result);
        }
        
        [Test]
        public void GetToplistModel_Authorized_ReturnsModel()
        {
            const string slug = "a";
            GetMock<IAuthentication>().Setup(o => o.GetUser()).Returns(new FakeUser());
            GetMock<ICashgameToplistPageModelFactory>().Setup(o => o.Create(It.IsAny<User>(), It.IsAny<Homegame>(), It.IsAny<CashgameSuite>(), It.IsAny<IList<int>>(), ToplistSortOrder.winnings, It.IsAny<int?>())).Returns(new CashgameToplistPageModel());

            var sut = GetSut();
            var result = sut.GetToplistModel(slug);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetDetailsModel_ReturnsModel()
        {
            const string slug = "a";
            const string dateStr = "2000-01-01"; 
            GetMock<IAuthenticationService>().Setup(o => o.GetUser()).Returns(new FakeUser());
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(It.IsAny<Homegame>(), It.IsAny<string>())).Returns(new FakeCashgame());
            GetMock<ICashgameDetailsPageModelFactory>().Setup(o => o.Create(It.IsAny<User>(), It.IsAny<Homegame>(), It.IsAny<Cashgame>(), It.IsAny<Player>(), It.IsAny<bool>())).Returns(new CashgameDetailsPageModel());

            var sut = GetSut();
            var result = sut.GetDetailsModel(slug, dateStr);

            Assert.IsNotNull(result);
        }

        private CashgameModelService GetSut()
        {
            return new CashgameModelService(
                GetMock<IHomegameRepository>().Object,
                GetMock<IAuthentication>().Object,
                GetMock<IMatrixPageModelFactory>().Object,
                GetMock<ICashgameService>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<ICashgameToplistPageModelFactory>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<ICashgameDetailsPageModelFactory>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<ICashgameDetailsChartModelFactory>().Object,
                GetMock<ICashgameFactsPageModelFactory>().Object,
                GetMock<IAddCashgamePageModelFactory>().Object,
                GetMock<ICashgameEditPageModelFactory>().Object,
                GetMock<IWebContext>().Object,
                GetMock<IRunningCashgamePageModelFactory>().Object,
                GetMock<ICashgameListPageModelFactory>().Object,
                GetMock<ICashgameChartPageModelFactory>().Object,
                GetMock<ICashgameSuiteChartModelFactory>().Object,
                GetMock<IActionPageModelFactory>().Object,
                GetMock<IActionChartModelFactory>().Object,
                GetMock<IBuyinPageModelFactory>().Object,
                GetMock<IReportPageModelFactory>().Object,
                GetMock<ICashoutPageModelFactory>().Object,
                GetMock<IEndPageModelFactory>().Object,
                GetMock<IEditCheckpointPageModelFactory>().Object,
                GetMock<ICheckpointRepository>().Object,
                GetMock<IAuthenticationService>().Object);
        }

    }
}
