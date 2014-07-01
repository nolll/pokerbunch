using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.AddCashgame;
using Core.Services.Interfaces;
using Web.Commands.CashgameCommands;
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
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Report;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class CashgameController : ControllerBase
    {
	    private readonly ICashgameService _cashgameService;
	    private readonly ICashgameCommandProvider _cashgameCommandProvider;
	    private readonly IToplistPageBuilder _toplistPageBuilder;
	    private readonly ICashgameFactsPageBuilder _cashgameFactsPageBuilder;
	    private readonly IActionPageBuilder _actionPageBuilder;
	    private readonly IMatrixPageBuilder _matrixPageBuilder;
	    private readonly ICashgameDetailsPageBuilder _cashgameDetailsPageBuilder;
	    private readonly ICashgameDetailsChartJsonBuilder _cashgameDetailsChartJsonBuilder;
	    private readonly IAddCashgamePageBuilder _addCashgamePageBuilder;
	    private readonly IAddCashgameInteractor _addCashgameInteractor;
	    private readonly IEditCashgamePageBuilder _editCashgamePageBuilder;
	    private readonly IRunningCashgamePageBuilder _runningCashgamePageBuilder;
	    private readonly ICashgameListPageBuilder _cashgameListPageBuilder;
	    private readonly ICashgameChartPageBuilder _cashgameChartPageBuilder;
	    private readonly ICashgameSuiteChartJsonBuilder _cashgameSuiteChartJsonBuilder;
	    private readonly IActionChartJsonBuilder _actionChartJsonBuilder;
	    private readonly IBuyinPageBuilder _buyinPageBuilder;
	    private readonly IReportPageBuilder _reportPageBuilder;
	    private readonly ICashoutPageBuilder _cashoutPageBuilder;
	    private readonly IEndPageBuilder _endPageBuilder;
	    private readonly IEditCheckpointPageBuilder _editCheckpointPageBuilder;

	    public CashgameController(
            ICashgameService cashgameService,
            ICashgameCommandProvider cashgameCommandProvider,
            IToplistPageBuilder toplistPageBuilder,
            ICashgameFactsPageBuilder cashgameFactsPageBuilder,
            IActionPageBuilder actionPageBuilder,
            IMatrixPageBuilder matrixPageBuilder,
            ICashgameDetailsPageBuilder cashgameDetailsPageBuilder,
            ICashgameDetailsChartJsonBuilder cashgameDetailsChartJsonBuilder,
            IAddCashgamePageBuilder addCashgamePageBuilder,
            IAddCashgameInteractor addCashgameInteractor,
            IEditCashgamePageBuilder editCashgamePageBuilder,
            IRunningCashgamePageBuilder runningCashgamePageBuilder,
            ICashgameListPageBuilder cashgameListPageBuilder,
            ICashgameChartPageBuilder cashgameChartPageBuilder,
            ICashgameSuiteChartJsonBuilder cashgameSuiteChartJsonBuilder,
            IActionChartJsonBuilder actionChartJsonBuilder,
            IBuyinPageBuilder buyinPageBuilder,
            IReportPageBuilder reportPageBuilder,
            ICashoutPageBuilder cashoutPageBuilder,
            IEndPageBuilder endPageBuilder,
            IEditCheckpointPageBuilder editCheckpointPageBuilder)
	    {
	        _cashgameService = cashgameService;
	        _cashgameCommandProvider = cashgameCommandProvider;
	        _toplistPageBuilder = toplistPageBuilder;
	        _cashgameFactsPageBuilder = cashgameFactsPageBuilder;
	        _actionPageBuilder = actionPageBuilder;
	        _matrixPageBuilder = matrixPageBuilder;
	        _cashgameDetailsPageBuilder = cashgameDetailsPageBuilder;
	        _cashgameDetailsChartJsonBuilder = cashgameDetailsChartJsonBuilder;
	        _addCashgamePageBuilder = addCashgamePageBuilder;
	        _addCashgameInteractor = addCashgameInteractor;
	        _editCashgamePageBuilder = editCashgamePageBuilder;
	        _runningCashgamePageBuilder = runningCashgamePageBuilder;
	        _cashgameListPageBuilder = cashgameListPageBuilder;
	        _cashgameChartPageBuilder = cashgameChartPageBuilder;
	        _cashgameSuiteChartJsonBuilder = cashgameSuiteChartJsonBuilder;
	        _actionChartJsonBuilder = actionChartJsonBuilder;
	        _buyinPageBuilder = buyinPageBuilder;
	        _reportPageBuilder = reportPageBuilder;
	        _cashoutPageBuilder = cashoutPageBuilder;
	        _endPageBuilder = endPageBuilder;
	        _editCheckpointPageBuilder = editCheckpointPageBuilder;
	    }

        [AuthorizePlayer]
	    public ActionResult Index(string slug)
        {
            var url = GetIndexUrl(slug);
            return Redirect(url.Relative);
		}

        private Url GetIndexUrl(string slug)
        {
            var year = _cashgameService.GetLatestYear(slug);
            if (year.HasValue)
            {
                return new CashgameMatrixUrl(slug, year);
            }
            return new AddCashgameUrl(slug);
        }

        [AuthorizePlayer]
        public ActionResult Matrix(string slug, int? year = null)
        {
            var model = _matrixPageBuilder.Build(slug, year);
			return View("Matrix/MatrixPage", model);
		}

        [AuthorizePlayer]
        public ActionResult Toplist(string slug, string orderBy = null, int? year = null)
        {
            var model = _toplistPageBuilder.Build(slug, orderBy, year);
            return View("Toplist/ToplistPage", model);
		}

        [AuthorizePlayer]
	    public ActionResult Details(string slug, string dateStr)
        {
            var model = _cashgameDetailsPageBuilder.Build(slug, dateStr);
			return View("Details/DetailsPage", model);
		}

        [AuthorizePlayer]
        public ActionResult DetailsChartJson(string slug, string dateStr)
        {
            var model = _cashgameDetailsChartJsonBuilder.Build(slug, dateStr);
		    return Json(model, JsonRequestBehavior.AllowGet);
		}

        [AuthorizePlayer]
        public ActionResult Facts(string slug, int? year = null)
        {
            var model = _cashgameFactsPageBuilder.Build(slug, year);
			return View("Facts/FactsPage", model);
		}

        [AuthorizePlayer]
        public ActionResult Add(string slug)
        {
            var model = _addCashgamePageBuilder.Build(slug);
            return View("Add/Add", model);
		}

        [HttpPost]
        [AuthorizePlayer]
        public ActionResult Add(string slug, AddCashgamePostModel postModel)
        {
            var request = new AddCashgameRequest(slug, postModel.Location);
            var result = _addCashgameInteractor.Execute(request);
            
            if (result.CreatedGame)
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            AddModelErrors(result.Errors);
            var model = _addCashgamePageBuilder.Build(slug, postModel);
            return View("Add/Add", model);
		}
        
	    [AuthorizeManager]
        public ActionResult Edit(string slug, string dateStr)
        {
            var model = _editCashgamePageBuilder.Build(slug, dateStr);
			return View("Edit/Edit", model);
		}

        [HttpPost]
        [AuthorizeManager]
        public ActionResult Edit(string slug, string dateStr, CashgameEditPostModel postModel)
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
        public ActionResult Running(string slug)
        {
            if(!_cashgameService.CashgameIsRunning(slug))
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
			}
            var model = _runningCashgamePageBuilder.Build(slug);
			return View("Running/RunningPage", model);
		}

        [AuthorizePlayer]
        public ActionResult List(string slug, int? year = null)
        {
            var model = _cashgameListPageBuilder.Build(slug, year);
            return View("List/List", model);
		}

        [AuthorizePlayer]
        public ActionResult Chart(string slug, int? year = null)
        {
            var model = _cashgameChartPageBuilder.Build(slug, year);
            return View("Chart/Chart", model);
		}

        [AuthorizePlayer]
        public JsonResult ChartJson(string slug, int? year = null)
        {
            var model = _cashgameSuiteChartJsonBuilder.Build(slug, year);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        [AuthorizePlayer]
        public ActionResult Action(string slug, string dateStr, int playerId)
        {
            var model = _actionPageBuilder.Build(slug, dateStr, playerId);
			return View("Action/Action", model);
		}

        [AuthorizePlayer]
		public JsonResult ActionChartJson(string slug, string dateStr, int playerId)
        {
            var model = _actionChartJsonBuilder.Build(slug, dateStr, playerId);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        [AuthorizeOwnPlayer]
        public ActionResult Buyin(string slug, int playerId)
        {
            var model = _buyinPageBuilder.Build(slug, playerId);
            return View("Buyin/Buyin", model);
		}

        [HttpPost]
        [AuthorizeOwnPlayer]
        public ActionResult Buyin(string slug, int playerId, BuyinPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetBuyinCommand(slug, playerId, postModel);
            if (command.Execute())
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _buyinPageBuilder.Build(slug, playerId, postModel);
            return View("Buyin/Buyin", model);
		}

        [AuthorizeOwnPlayer]
        public ActionResult Report(string slug, int playerId)
        {
            var model = _reportPageBuilder.Build(slug);
            return View("Report/Report", model);
		}

        [HttpPost]
        [AuthorizeOwnPlayer]
        public ActionResult Report(string slug, int playerId, ReportPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetReportCommand(slug, playerId, postModel);
            if (command.Execute())
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _reportPageBuilder.Build(slug, postModel);
            return View("Report/Report", model);
		}

        [AuthorizeManager]
        public ActionResult EditCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var model = _editCheckpointPageBuilder.Build(slug, dateStr, playerId, checkpointId);
            return View("Checkpoints/Edit", model);
        }

        [HttpPost]
        [AuthorizeManager]
        public ActionResult EditCheckpoint(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
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
        public ActionResult DeleteCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var command = _cashgameCommandProvider.GetDeleteCheckpointCommand(slug, dateStr, checkpointId);
            if (command.Execute())
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            var actionsUrl = new CashgameActionUrl(slug, dateStr, playerId);
            return Redirect(actionsUrl.Relative);
		}
        
        [AuthorizeOwnPlayer]
        public ActionResult Cashout(string slug, int playerId)
        {
            var model = _cashoutPageBuilder.Build(slug);
            return View("Cashout/Cashout", model);
		}

        [HttpPost]
        [AuthorizeOwnPlayer]
        public ActionResult Cashout(string slug, int playerId, CashoutPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetCashoutCommand(slug, playerId, postModel);
            if (command.Execute())
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _cashoutPageBuilder.Build(slug, postModel);
            return View("Cashout/Cashout", model);
		}

        [AuthorizePlayer]
        public ActionResult End(string slug)
        {
            var model = _endPageBuilder.Build(slug);
			return View("End/End", model);
		}

        [HttpPost]
        [AuthorizePlayer]
        public ActionResult End(string slug, EndPageModel postModel)
        {
            var command = _cashgameCommandProvider.GetEndGameCommand(slug);
            command.Execute();
            return Redirect(new CashgameIndexUrl(slug).Relative);
		}

        [AuthorizeManager]
        public ActionResult Delete(string slug, string dateStr)
        {
            var command = _cashgameCommandProvider.GetDeleteCommand(slug, dateStr);
            command.Execute();
            return Redirect(new CashgameIndexUrl(slug).Relative);
		}
	}
}