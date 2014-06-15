using Application.Services;
using Core.Entities;
using Core.Repositories;
using Core.Services.Interfaces;
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
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelServices;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Matrix;
using Web.Models.UrlModels;

namespace Tests.Web.ModelServiceTests
{
    public class CashgameModelServiceTests : MockContainer
    {
        [Test]
        public void GetIndexUrl_CashgameServiceReturnsYear_ReturnsMatrixUrl()
        {
            const string slug = "a";
            const int year = 1;

            GetMock<ICashgameService>().Setup(o => o.GetLatestYear(slug)).Returns(year);

            var sut = GetSut();
            var result = sut.GetIndexUrl(slug);

            Assert.IsInstanceOf<CashgameMatrixUrl>(result);
        }

        [Test]
        public void GetIndexUrl_CashgameServiceReturnsNoYear_ReturnsAddCashgameUrl()
        {
            const string slug = "a";

            var sut = GetSut();
            var result = sut.GetIndexUrl(slug);

            Assert.IsInstanceOf<AddCashgameUrl>(result);
        }

        [Test]
        public void GetMatrixModel_Authorized_ReturnsModel()
        {
            const string slug = "a";
            GetMock<IAuth>().Setup(o => o.CurrentUser).Returns(new UserInTest());
            GetMock<IMatrixPageModelFactory>().Setup(o => o.Create(It.IsAny<Homegame>(), It.IsAny<int?>())).Returns(new CashgameMatrixPageModel());

            var sut = GetSut();
            var result = sut.GetMatrixModel(slug);

            Assert.IsNotNull(result);
        }
        
        [Test]
        public void GetDetailsModel_ReturnsModel()
        {
            const string slug = "a";
            const string dateStr = "2000-01-01"; 
            GetMock<IAuth>().Setup(o => o.CurrentUser).Returns(new UserInTest());
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(It.IsAny<Homegame>(), It.IsAny<string>())).Returns(new CashgameInTest());
            GetMock<ICashgameDetailsPageModelFactory>().Setup(o => o.Create(It.IsAny<Homegame>(), It.IsAny<Cashgame>(), It.IsAny<Player>(), It.IsAny<bool>())).Returns(new CashgameDetailsPageModel());

            var sut = GetSut();
            var result = sut.GetDetailsModel(slug, dateStr);

            Assert.IsNotNull(result);
        }

        private CashgameModelService GetSut()
        {
            return new CashgameModelService(
                GetMock<IHomegameRepository>().Object,
                GetMock<IAuth>().Object,
                GetMock<IMatrixPageModelFactory>().Object,
                GetMock<ICashgameService>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<ICashgameDetailsPageModelFactory>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<ICashgameDetailsChartModelFactory>().Object,
                GetMock<IAddCashgamePageModelFactory>().Object,
                GetMock<ICashgameEditPageModelFactory>().Object,
                GetMock<IWebContext>().Object,
                GetMock<IRunningCashgamePageModelFactory>().Object,
                GetMock<ICashgameListPageModelFactory>().Object,
                GetMock<ICashgameChartPageModelFactory>().Object,
                GetMock<ICashgameSuiteChartModelFactory>().Object,
                GetMock<IActionPageBuilder>().Object,
                GetMock<IActionChartModelFactory>().Object,
                GetMock<IBuyinPageModelFactory>().Object,
                GetMock<IReportPageModelFactory>().Object,
                GetMock<ICashoutPageModelFactory>().Object,
                GetMock<IEndPageModelFactory>().Object,
                GetMock<IEditCheckpointPageModelFactory>().Object,
                GetMock<ICheckpointRepository>().Object);
        }

    }
}
