using System;
using System.Web.Mvc;
using NUnit.Framework;
using Tests.Common;
using Web.Commands.CashgameCommands;
using Web.Controllers;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Checkpoints;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Running;

namespace Tests.Web.ControllerTests
{
	public class CashgameControllerTests : TestBase
	{
        [Test]
        public void Action_RequiresPlayer()
        {
            var sut = GetSut();
            Func<string, string, int, ActionResult> methodToTest = sut.Action;
            var result = SecurityTestHelper.RequiresPlayer(methodToTest);

            Assert.IsTrue(result);
        }

        [Test]
        public void Buyin_RequiresOwnPlayer()
        {
            var sut = GetSut();
            Func<string, int, ActionResult> methodToTest = sut.Buyin;
            var result = SecurityTestHelper.RequiresOwnPlayer(methodToTest);

            Assert.IsTrue(result);
        }

        private CashgameController GetSut()
        {
            return new CashgameController(
                GetMock<ICashgameCommandProvider>().Object,
                GetMock<IMatrixPageBuilder>().Object,
                GetMock<ICashgameDetailsChartJsonBuilder>().Object,
                GetMock<IEditCashgamePageBuilder>().Object,
                GetMock<IRunningCashgamePageBuilder>().Object,
                GetMock<ICashgameListPageBuilder>().Object,
                GetMock<ICashgameChartPageBuilder>().Object,
                GetMock<ICashgameSuiteChartJsonBuilder>().Object,
                GetMock<IActionChartJsonBuilder>().Object,
                GetMock<IEditCheckpointPageBuilder>().Object);
        }
	}
}