using System;
using System.Web.Mvc;
using Application.UseCases.Actions;
using Application.UseCases.AddCashgame;
using Application.UseCases.AddCashgameForm;
using Application.UseCases.BunchContext;
using Application.UseCases.Buyin;
using Application.UseCases.BuyinForm;
using Application.UseCases.CashgameDetails;
using Application.UseCases.CashgameFacts;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeInteractors;
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
	public class CashgameControllerTests : MockContainer
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
                GetMock<IBunchContextInteractor>().Object,
                GetMock<IAddCashgameFormInteractor>().Object,
                GetMock<ICashgameCommandProvider>().Object,
                GetMock<IMatrixPageBuilder>().Object,
                GetMock<ICashgameDetailsChartJsonBuilder>().Object,
                GetMock<IAddCashgameInteractor>().Object,
                GetMock<IEditCashgamePageBuilder>().Object,
                GetMock<IRunningCashgamePageBuilder>().Object,
                GetMock<ICashgameListPageBuilder>().Object,
                GetMock<ICashgameChartPageBuilder>().Object,
                GetMock<ICashgameSuiteChartJsonBuilder>().Object,
                GetMock<IActionChartJsonBuilder>().Object,
                GetMock<IEditCheckpointPageBuilder>().Object,
                new CashgameContextInteractorInTest(),
                new TopListInteractorInTest(),
                GetMock<ICashgameFactsInteractor>().Object,
                GetMock<IActionsInteractor>().Object,
                GetMock<ICashgameDetailsInteractor>().Object,
                GetMock<IBuyinFormInteractor>().Object,
                GetMock<IBuyinInteractor>().Object);
        }
	}
}