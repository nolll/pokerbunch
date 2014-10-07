using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases.Actions;
using Core.UseCases.BunchContext;
using Core.UseCases.Buyin;
using Core.UseCases.BuyinForm;
using Core.UseCases.CashgameChartContainer;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameList;
using Core.UseCases.EditCheckpointForm;
using Core.UseCases.RunningCashgame;
using Web.Commands.CashgameCommands;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.Models.CashgameModels.Action;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Chart;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.List;
using Web.Models.CashgameModels.Report;
using Web.Models.CashgameModels.Running;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EditCashgameController : ControllerBase
    {
        private readonly IEditCashgamePageBuilder _editCashgamePageBuilder;
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public EditCashgameController(IEditCashgamePageBuilder editCashgamePageBuilder, ICashgameCommandProvider cashgameCommandProvider)
        {
            _editCashgamePageBuilder = editCashgamePageBuilder;
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeManager]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Edit(string slug, string dateStr)
        {
            var model = _editCashgamePageBuilder.Build(slug, dateStr);
			return View("EditCashgame/Edit", model);
		}

	    [HttpPost]
        [AuthorizeManager]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Edit_Post(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetEditCommand(slug, dateStr, postModel);
            if (command.Execute())
            {
                return Redirect(new CashgameDetailsUrl(slug, dateStr).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _editCashgamePageBuilder.Build(slug, dateStr, postModel);
            return View("EditCashgame/Edit", model);
		} 
    }

    public class RunningCashgameController : ControllerBase
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/running")]
        public ActionResult Running(string slug)
        {
            try
            {
                var contextResult = UseCase.CashgameContext(new CashgameContextRequest(slug));
                var runningCashgameResult = UseCase.RunningCashgame(new RunningCashgameRequest(slug));
                var model = new RunningCashgamePageModel(contextResult, runningCashgameResult);
                return View("RunningCashgame/RunningPage", model);
            }
            catch (CashgameNotRunningException)
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
            }
        }
    }

    public class CashgameListController : ControllerBase
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/list/{year?}")]
        public ActionResult List(string slug, int? year = null, string orderBy = null)
        {
            var contextResult = UseCase.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.List));
            var listResult = UseCase.CashgameList(new CashgameListRequest(slug, orderBy, year));

            var model = new CashgameListPageModel(contextResult, listResult);
            return View("CashgameList/List", model);
        }
    }

    public class CashgameChartController : ControllerBase
    {
        private readonly ICashgameSuiteChartJsonBuilder _cashgameSuiteChartJsonBuilder;

        public CashgameChartController(ICashgameSuiteChartJsonBuilder cashgameSuiteChartJsonBuilder)
        {
            _cashgameSuiteChartJsonBuilder = cashgameSuiteChartJsonBuilder;
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/chart/{year?}")]
        public ActionResult Chart(string slug, int? year = null)
        {
            var cashgameContextResult = UseCase.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Chart));
            var cashgameChartContainerResult = UseCase.CashgameChartContainer(new CashgameChartContainerRequest(slug, year));
            var model = new CashgameChartPageModel(cashgameContextResult, cashgameChartContainerResult);
            return View("CashgameChart/Chart", model);
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/chartjson/{year?}")]
        public JsonResult ChartJson(string slug, int? year = null)
        {
            var model = _cashgameSuiteChartJsonBuilder.Build(slug, year);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }

    public class CashgameActionController : ControllerBase
    {
        private readonly IActionChartJsonBuilder _actionChartJsonBuilder;

        public CashgameActionController(IActionChartJsonBuilder actionChartJsonBuilder)
        {
            _actionChartJsonBuilder = actionChartJsonBuilder;
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/action/{dateStr}/{playerId:int}")]
        public ActionResult Action(string slug, string dateStr, int playerId)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var actionsResult = UseCase.Actions(new ActionsRequest(slug, dateStr, playerId));
            var model = new ActionPageModel(contextResult, actionsResult);
            return View("CashgameAction/Action", model);
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/actionchartjson/{dateStr}/{playerId:int}")]
        public JsonResult ActionChartJson(string slug, string dateStr, int playerId)
        {
            var model = _actionChartJsonBuilder.Build(slug, dateStr, playerId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }

    public class CashgameBuyinController : ControllerBase
    {
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/buyin/{playerId:int}")]
        public ActionResult Buyin(string slug, int playerId)
        {
            var model = BuildBuyinModel(slug, playerId);
            return View("CashgameBuyin/Buyin", model);
        }

        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/buyin/{playerId:int}")]
        public ActionResult Buyin_Post(string slug, int playerId, BuyinPostModel postModel)
        {
            var request = new BuyinRequest(slug, playerId, postModel.BuyinAmount, postModel.StackAmount);

            try
            {
                var result = UseCase.Buyin(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            var model = BuildBuyinModel(slug, playerId, postModel);
            return View("CashgameBuyin/Buyin", model);
        }

        private BuyinPageModel BuildBuyinModel(string slug, int playerId, BuyinPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var buyinFormResult = UseCase.BuyinForm(new BuyinFormRequest(slug, playerId));
            return new BuyinPageModel(contextResult, buyinFormResult, postModel);
        }
    }

    public class CashgameReportController : ControllerBase
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public CashgameReportController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/report/{playerId:int}")]
        public ActionResult Report(string slug, int playerId)
        {
            var model = BuildReportModel(slug);
            return View("CashgameReport/Report", model);
        }

        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/report/{playerId:int}")]
        public ActionResult Report_Post(string slug, int playerId, ReportPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetReportCommand(slug, playerId, postModel);
            if (command.Execute())
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildReportModel(slug, postModel);
            return View("CashgameReport/Report", model);
        }

        private ReportPageModel BuildReportModel(string slug, ReportPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            return new ReportPageModel(contextResult, postModel);
        }
    }

    public class EditCheckpointController : ControllerBase
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public EditCheckpointController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeManager]
        [Route("{slug}/cashgame/editcheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult EditCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var model = BuildEditCheckpointModel(slug, dateStr, playerId, checkpointId);
            return View("EditCheckpoint/Edit", model);
        }

        [HttpPost]
        [AuthorizeManager]
        [Route("{slug}/cashgame/editcheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult EditCheckpoint_Post(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetEditCheckpointCommand(slug, dateStr, checkpointId, postModel);
            if (command.Execute())
            {
                return Redirect(new CashgameActionUrl(slug, dateStr, playerId).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildEditCheckpointModel(slug, dateStr, playerId, checkpointId, postModel);
            return View("EditCheckpoint/Edit", model);
        }

        private EditCheckpointPageModel BuildEditCheckpointModel(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var editCheckpointFormResult = UseCase.EditCheckpointForm(new EditCheckpointFormRequest(slug, dateStr, playerId, checkpointId));
            return new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel);
        }
    }

    public class DeleteCheckpointController : ControllerBase
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public DeleteCheckpointController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeManager]
        [Route("{slug}/cashgame/deletecheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult DeleteCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var command = _cashgameCommandProvider.GetDeleteCheckpointCommand(slug, dateStr, checkpointId);
            if (command.Execute())
            {
                // if the cashgame isn't running, this should redirect back to the cashgame details
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            var actionsUrl = new CashgameActionUrl(slug, dateStr, playerId);
            return Redirect(actionsUrl.Relative);
        }
    }

    public class CashgameCashoutController : ControllerBase
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public CashgameCashoutController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/cashout/{playerId:int}")]
        public ActionResult Cashout(string slug, int playerId)
        {
            var model = BuildCashoutModel(slug);
            return View("CashgameCashout/Cashout", model);
        }

        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/cashout/{playerId:int}")]
        public ActionResult Cashout_Post(string slug, int playerId, CashoutPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetCashoutCommand(slug, playerId, postModel);
            if (command.Execute())
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildCashoutModel(slug, postModel);
            return View("CashgameCashout/Cashout", model);
        }

        private CashoutPageModel BuildCashoutModel(string slug, CashoutPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            return new CashoutPageModel(contextResult, postModel);
        }
    }

    public class EndCashgameController : ControllerBase
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public EndCashgameController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/end")]
        public ActionResult End(string slug)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new EndGamePageModel(contextResult);
            return View("EndCashgame/End", model);
        }

        [HttpPost]
        [AuthorizePlayer]
        [Route("{slug}/cashgame/end")]
        public ActionResult End_Post(string slug, EndGamePostModel postModel)
        {
            var command = _cashgameCommandProvider.GetEndGameCommand(slug);
            command.Execute();
            return Redirect(new CashgameIndexUrl(slug).Relative);
        }
    }

    public class DeleteCashgameController : ControllerBase
    {
	    private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public DeleteCashgameController(ICashgameCommandProvider cashgameCommandProvider)
	    {
	        _cashgameCommandProvider = cashgameCommandProvider;
	    }

        [AuthorizeManager]
        [Route("{slug}/cashgame/delete/{dateStr}")]
        public ActionResult Delete(string slug, string dateStr)
        {
            var command = _cashgameCommandProvider.GetDeleteCommand(slug, dateStr);
            command.Execute();
            return Redirect(new CashgameIndexUrl(slug).Relative);
		}
    }
}