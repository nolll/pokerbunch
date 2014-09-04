using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.Actions;
using Application.UseCases.AddCashgame;
using Application.UseCases.AddCashgameForm;
using Application.UseCases.BunchContext;
using Application.UseCases.Buyin;
using Application.UseCases.BuyinForm;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameDetails;
using Application.UseCases.CashgameFacts;
using Application.UseCases.CashgameTopList;
using Web.Commands.CashgameCommands;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Checkpoints;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.Models.CashgameModels.Action;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Facts;
using Web.Models.CashgameModels.Report;
using Web.Models.CashgameModels.Toplist;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class CashgameController : ControllerBase
    {
	    private readonly IBunchContextInteractor _bunchContextInteractor;
	    private readonly IAddCashgameFormInteractor _addCashgameFormInteractor;
	    private readonly ICashgameCommandProvider _cashgameCommandProvider;
	    private readonly IMatrixPageBuilder _matrixPageBuilder;
	    private readonly ICashgameDetailsChartJsonBuilder _cashgameDetailsChartJsonBuilder;
	    private readonly IAddCashgameInteractor _addCashgameInteractor;
	    private readonly IEditCashgamePageBuilder _editCashgamePageBuilder;
	    private readonly IRunningCashgamePageBuilder _runningCashgamePageBuilder;
	    private readonly ICashgameListPageBuilder _cashgameListPageBuilder;
	    private readonly ICashgameChartPageBuilder _cashgameChartPageBuilder;
	    private readonly ICashgameSuiteChartJsonBuilder _cashgameSuiteChartJsonBuilder;
	    private readonly IActionChartJsonBuilder _actionChartJsonBuilder;
	    private readonly IEditCheckpointPageBuilder _editCheckpointPageBuilder;
	    private readonly ICashgameContextInteractor _cashgameContextInteractor;
	    private readonly ITopListInteractor _topListInteractor;
	    private readonly ICashgameFactsInteractor _cashgameFactsInteractor;
	    private readonly IActionsInteractor _actionsInteractor;
	    private readonly ICashgameDetailsInteractor _cashgameDetailsInteractor;
	    private readonly IBuyinFormInteractor _buyinFormInteractor;
	    private readonly IBuyinInteractor _buyinInteractor;

	    public CashgameController(
            IBunchContextInteractor bunchContextInteractor,
            IAddCashgameFormInteractor addCashgameFormInteractor,
            ICashgameCommandProvider cashgameCommandProvider,
            IMatrixPageBuilder matrixPageBuilder,
            ICashgameDetailsChartJsonBuilder cashgameDetailsChartJsonBuilder,
            IAddCashgameInteractor addCashgameInteractor,
            IEditCashgamePageBuilder editCashgamePageBuilder,
            IRunningCashgamePageBuilder runningCashgamePageBuilder,
            ICashgameListPageBuilder cashgameListPageBuilder,
            ICashgameChartPageBuilder cashgameChartPageBuilder,
            ICashgameSuiteChartJsonBuilder cashgameSuiteChartJsonBuilder,
            IActionChartJsonBuilder actionChartJsonBuilder,
            IEditCheckpointPageBuilder editCheckpointPageBuilder,
            ICashgameContextInteractor cashgameContextInteractor,
            ITopListInteractor topListInteractor,
            ICashgameFactsInteractor cashgameFactsInteractor,
            IActionsInteractor actionsInteractor,
            ICashgameDetailsInteractor cashgameDetailsInteractor,
            IBuyinFormInteractor buyinFormInteractor,
            IBuyinInteractor buyinInteractor)
	    {
	        _bunchContextInteractor = bunchContextInteractor;
	        _addCashgameFormInteractor = addCashgameFormInteractor;
	        _cashgameCommandProvider = cashgameCommandProvider;
	        _matrixPageBuilder = matrixPageBuilder;
	        _cashgameDetailsChartJsonBuilder = cashgameDetailsChartJsonBuilder;
	        _addCashgameInteractor = addCashgameInteractor;
	        _editCashgamePageBuilder = editCashgamePageBuilder;
	        _runningCashgamePageBuilder = runningCashgamePageBuilder;
	        _cashgameListPageBuilder = cashgameListPageBuilder;
	        _cashgameChartPageBuilder = cashgameChartPageBuilder;
	        _cashgameSuiteChartJsonBuilder = cashgameSuiteChartJsonBuilder;
	        _actionChartJsonBuilder = actionChartJsonBuilder;
	        _editCheckpointPageBuilder = editCheckpointPageBuilder;
	        _cashgameContextInteractor = cashgameContextInteractor;
	        _topListInteractor = topListInteractor;
	        _cashgameFactsInteractor = cashgameFactsInteractor;
	        _actionsInteractor = actionsInteractor;
	        _cashgameDetailsInteractor = cashgameDetailsInteractor;
	        _buyinFormInteractor = buyinFormInteractor;
	        _buyinInteractor = buyinInteractor;
	    }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/index")]
        public ActionResult Index(string slug)
        {
            var result = _cashgameContextInteractor.Execute(new CashgameContextRequest(slug));
            var url = GetIndexUrl(result);
            return Redirect(url.Relative);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/matrix/{year?}")]
        public ActionResult Matrix(string slug, int? year = null)
        {
            var model = _matrixPageBuilder.Build(slug, year);
			return View("Matrix/MatrixPage", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/toplist/{year?}")]
        public ActionResult Toplist(string slug, string orderBy = null, int? year = null)
        {
            var contextResult = _cashgameContextInteractor.Execute(new CashgameContextRequest(slug, year, CashgamePage.Toplist));
            var topListResult = _topListInteractor.Execute(new TopListRequest(slug, orderBy, year));
            var model = new CashgameToplistPageModel(contextResult, topListResult);
            return View("Toplist/ToplistPage", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/details/{dateStr}")]
        public ActionResult Details(string slug, string dateStr)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
            var cashgameDetailsResult = _cashgameDetailsInteractor.Execute(new CashgameDetailsRequest(slug, dateStr));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult);
			return View("Details/DetailsPage", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/detailschartjson/{dateStr}")]
        public ActionResult DetailsChartJson(string slug, string dateStr)
        {
            var model = _cashgameDetailsChartJsonBuilder.Build(slug, dateStr);
		    return Json(model, JsonRequestBehavior.AllowGet);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/facts/{year?}")]
        public ActionResult Facts(string slug, int? year = null)
        {
            var contextResult = _cashgameContextInteractor.Execute(new CashgameContextRequest(slug, year, CashgamePage.Facts));
            var factsResult = _cashgameFactsInteractor.Execute(new CashgameFactsRequest(slug, year));

            var model = new CashgameFactsPageModel(contextResult, factsResult);
			return View("Facts/FactsPage", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/add")]
        public ActionResult Add(string slug)
        {
            var model = BuildAddModel(slug);
            return View("Add/Add", model);
		}

	    [HttpPost]
        [AuthorizePlayer]
        [Route("{slug}/cashgame/add")]
        public ActionResult Add_Post(string slug, AddCashgamePostModel postModel)
        {
            var request = new AddCashgameRequest(slug, postModel.Location);
            var result = _addCashgameInteractor.Execute(request);
            
            if (result.Success)
                return Redirect(result.ReturnUrl.Relative);

            AddModelErrors(result.Errors);
            var model = BuildAddModel(slug, postModel);
            return View("Add/Add", model);
		}

	    [AuthorizeManager]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Edit(string slug, string dateStr)
        {
            var model = _editCashgamePageBuilder.Build(slug, dateStr);
			return View("Edit/Edit", model);
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
            return View("Edit/Edit", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/running")]
        public ActionResult Running(string slug)
        {
            var result = _cashgameContextInteractor.Execute(new CashgameContextRequest(slug));
            if(!result.GameIsRunning)
                return Redirect(new CashgameIndexUrl(slug).Relative);
            var model = _runningCashgamePageBuilder.Build(slug);
			return View("Running/RunningPage", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/list/{year?}")]
        public ActionResult List(string slug, int? year = null)
        {
            var model = _cashgameListPageBuilder.Build(slug, year);
            return View("List/List", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/chart/{year?}")]
        public ActionResult Chart(string slug, int? year = null)
        {
            var model = _cashgameChartPageBuilder.Build(slug, year);
            return View("Chart/Chart", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/chartjson/{year?}")]
        public JsonResult ChartJson(string slug, int? year = null)
        {
            var model = _cashgameSuiteChartJsonBuilder.Build(slug, year);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/action/{dateStr}/{playerId:int}")]
        public ActionResult Action(string slug, string dateStr, int playerId)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
            var actionsResult = _actionsInteractor.Execute(new ActionsRequest(slug, dateStr, playerId));
            var model = new ActionPageModel(contextResult, actionsResult);
			return View("Action/Action", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/actionchartjson/{dateStr}/{playerId:int}")]
        public JsonResult ActionChartJson(string slug, string dateStr, int playerId)
        {
            var model = _actionChartJsonBuilder.Build(slug, dateStr, playerId);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

	    [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/buyin/{playerId:int}")]
        public ActionResult Buyin(string slug, int playerId)
        {
            var model = BuildBuyinModel(slug, playerId);
            return View("Buyin/Buyin", model);
		}

	    [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/buyin/{playerId:int}")]
        public ActionResult Buyin_Post(string slug, int playerId, BuyinPostModel postModel)
        {
            var request = new BuyinRequest(slug, playerId, postModel.BuyinAmount, postModel.StackAmount);
            var result = _buyinInteractor.Execute(request);

            if (result.Success)
                return Redirect(result.ReturnUrl.Relative);

            AddModelErrors(result.Errors);
            var model = BuildBuyinModel(slug, playerId, postModel);
            return View("Buyin/Buyin", model);
		}

	    [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/report/{playerId:int}")]
        public ActionResult Report(string slug, int playerId)
        {
            var model = BuildReportModel(slug);
            return View("Report/Report", model);
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
            return View("Report/Report", model);
		}

	    [AuthorizeManager]
        [Route("{slug}/cashgame/editcheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult EditCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var model = _editCheckpointPageBuilder.Build(slug, dateStr, playerId, checkpointId);
            return View("Checkpoints/Edit", model);
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
            var model = _editCheckpointPageBuilder.Build(slug, dateStr, playerId, checkpointId, postModel);
            return View("Checkpoints/Edit", model);
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

	    [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/cashout/{playerId:int}")]
        public ActionResult Cashout(string slug, int playerId)
        {
            var model = BuildCashoutModel(slug);
            return View("Cashout/Cashout", model);
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
            return View("Cashout/Cashout", model);
		}

	    [AuthorizePlayer]
        [Route("{slug}/cashgame/end")]
        public ActionResult End(string slug)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
            var model = new EndGamePageModel(contextResult);
			return View("End/End", model);
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

	    [AuthorizeManager]
        [Route("{slug}/cashgame/delete/{dateStr}")]
        public ActionResult Delete(string slug, string dateStr)
        {
            var command = _cashgameCommandProvider.GetDeleteCommand(slug, dateStr);
            command.Execute();
            return Redirect(new CashgameIndexUrl(slug).Relative);
		}

	    private Url GetIndexUrl(CashgameContextResult result)
	    {
	        if (result.LatestYear.HasValue)
	            return new CashgameMatrixUrl(result.BunchContext.Slug, result.LatestYear);
	        return new AddCashgameUrl(result.BunchContext.Slug);
	    }

	    private AddCashgamePageModel BuildAddModel(string slug, AddCashgamePostModel postModel = null)
	    {
	        var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
	        var optionsResult = _addCashgameFormInteractor.Execute(new AddCashgameFormRequest(slug));
	        return new AddCashgamePageModel(contextResult, optionsResult, postModel);
	    }

	    private BuyinPageModel BuildBuyinModel(string slug, int playerId, BuyinPostModel postModel = null)
	    {
	        var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
	        var buyinFormResult = _buyinFormInteractor.Execute(new BuyinFormRequest(slug, playerId));
	        return new BuyinPageModel(contextResult, buyinFormResult, postModel);
	    }

	    private ReportPageModel BuildReportModel(string slug, ReportPostModel postModel = null)
	    {
	        var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
	        return new ReportPageModel(contextResult, postModel);
	    }

	    private CashoutPageModel BuildCashoutModel(string slug, CashoutPostModel postModel = null)
	    {
	        var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
	        return new CashoutPageModel(contextResult, postModel);
	    }
    }
}