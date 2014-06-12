using System.Web.Mvc;
using Application.Services;
using Core.Services.Interfaces;
using Web.Commands.CashgameCommands;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.ModelServices;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Report;
using Web.Models.UrlModels;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class CashgameController : ControllerBase
    {
	    private readonly ICashgameService _cashgameService;
	    private readonly ICashgameCommandProvider _cashgameCommandProvider;
	    private readonly ICashgameModelService _cashgameModelService;
	    private readonly IToplistPageBuilder _toplistPageBuilder;
	    private readonly ICashgameFactsPageBuilder _cashgameFactsPageBuilder;

	    public CashgameController(
            ICashgameService cashgameService,
            ICashgameCommandProvider cashgameCommandProvider,
            ICashgameModelService cashgameModelService,
            IToplistPageBuilder toplistPageBuilder,
            ICashgameFactsPageBuilder cashgameFactsPageBuilder)
	    {
	        _cashgameService = cashgameService;
	        _cashgameCommandProvider = cashgameCommandProvider;
	        _cashgameModelService = cashgameModelService;
	        _toplistPageBuilder = toplistPageBuilder;
	        _cashgameFactsPageBuilder = cashgameFactsPageBuilder;
	    }

        [AuthorizePlayer]
	    public ActionResult Index(string slug)
	    {
            var url = _cashgameModelService.GetIndexUrl(slug);
            return Redirect(url.Relative);
		}

        [AuthorizePlayer]
        public ActionResult Matrix(string slug, int? year = null)
        {
            var model = _cashgameModelService.GetMatrixModel(slug, year);
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
            var model = _cashgameModelService.GetDetailsModel(slug, dateStr);
			return View("Details/DetailsPage", model);
		}

        [AuthorizePlayer]
        public ActionResult DetailsChartJson(string slug, string dateStr)
        {
            var model = _cashgameModelService.GetDetailsChartJsonModel(slug, dateStr);
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
            var model = _cashgameModelService.GetAddModel(slug);
            return View("Add/Add", model);
		}

        [HttpPost]
        [AuthorizePlayer]
        public ActionResult Add(string slug, AddCashgamePostModel postModel)
        {
            var command = _cashgameCommandProvider.GetAddCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _cashgameModelService.GetAddModel(slug, postModel);
            return View("Add/Add", model);
		}

        [AuthorizeManager]
        public ActionResult Edit(string slug, string dateStr)
        {
            var model = _cashgameModelService.GetEditModel(slug, dateStr);
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
            var model = _cashgameModelService.GetEditModel(slug, dateStr, postModel);
            return View("Edit/Edit", model);
		}

        [AuthorizePlayer]
        public ActionResult Running(string slug)
        {
            if(!_cashgameService.CashgameIsRunning(slug))
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
			}
			var model = _cashgameModelService.GetRunningModel(slug);
			return View("Running/RunningPage", model);
		}

        [AuthorizePlayer]
        public ActionResult List(string slug, int? year = null)
        {
            var model = _cashgameModelService.GetListModel(slug, year);
            return View("List/List", model);
		}

        [AuthorizePlayer]
        public ActionResult Chart(string slug, int? year = null)
        {
            var model = _cashgameModelService.GetChartModel(slug, year);
            return View("Chart/Chart", model);
		}

        [AuthorizePlayer]
        public JsonResult ChartJson(string slug, int? year = null)
        {
            var model = _cashgameModelService.GetChartJsonModel(slug, year);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        [AuthorizePlayer]
        public ActionResult Action(string slug, string dateStr, int playerId)
        {
            var model = _cashgameModelService.GetActionModel(slug, dateStr, playerId);
			return View("Action/Action", model);
		}

        [AuthorizePlayer]
		public JsonResult ActionChartJson(string slug, string dateStr, int playerId)
        {
		    var model = _cashgameModelService.GetActionChartJsonModel(slug, dateStr, playerId);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        [AuthorizeOwnPlayer]
        public ActionResult Buyin(string slug, int playerId)
        {
            var model = _cashgameModelService.GetBuyinModel(slug, playerId);
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
            var model = _cashgameModelService.GetBuyinModel(slug, playerId, postModel);
            return View("Buyin/Buyin", model);
		}

        [AuthorizeOwnPlayer]
        public ActionResult Report(string slug, int playerId)
        {
            var model = _cashgameModelService.GetReportModel(slug);
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
            var model = _cashgameModelService.GetReportModel(slug, postModel);
            return View("Report/Report", model);
		}

        [AuthorizeManager]
        public ActionResult EditCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var model = _cashgameModelService.GetEditCheckpointModel(slug, dateStr, playerId, checkpointId);
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
            var model = _cashgameModelService.GetEditCheckpointModel(slug, dateStr, playerId, checkpointId, postModel);
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
            var model = _cashgameModelService.GetCashoutModel(slug);
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
            var model = _cashgameModelService.GetCashoutModel(slug, postModel);
            return View("Cashout/Cashout", model);
		}

        [AuthorizePlayer]
        public ActionResult End(string slug)
        {
            var model = _cashgameModelService.GetEndGameModel(slug);
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