using System.Web.Mvc;
using Application.Services;
using Core.Services.Interfaces;
using Web.Commands.CashgameCommands;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.ModelServices;
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
	    private readonly IUrlProvider _urlProvider;
	    private readonly ICashgameService _cashgameService;
	    private readonly ICashgameCommandProvider _cashgameCommandProvider;
	    private readonly ICashgameModelService _cashgameModelService;
	    private readonly ICashgameToplistPageBuilder _cashgameToplistPageBuilder;

	    public CashgameController(
            IUrlProvider urlProvider,
            ICashgameService cashgameService,
            ICashgameCommandProvider cashgameCommandProvider,
            ICashgameModelService cashgameModelService,
            ICashgameToplistPageBuilder cashgameToplistPageBuilder)
	    {
	        _urlProvider = urlProvider;
	        _cashgameService = cashgameService;
	        _cashgameCommandProvider = cashgameCommandProvider;
	        _cashgameModelService = cashgameModelService;
	        _cashgameToplistPageBuilder = cashgameToplistPageBuilder;
	    }

        [AuthorizePlayer]
	    public ActionResult Index(string slug)
	    {
            var url = _cashgameModelService.GetIndexUrl(slug);
            return Redirect(url);
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
            var model = _cashgameToplistPageBuilder.Build(slug, orderBy, year);
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
            var model = _cashgameModelService.GetFactsModel(slug, year);
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
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
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
                return Redirect(_urlProvider.GetCashgameDetailsUrl(slug, dateStr));
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
                return Redirect(_urlProvider.GetCashgameIndexUrl(slug));
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
        public ActionResult Action(string slug, string dateStr, string playerName)
        {
            var model = _cashgameModelService.GetActionModel(slug, dateStr, playerName);
			return View("Action/Action", model);
		}

        [AuthorizePlayer]
		public JsonResult ActionChartJson(string slug, string dateStr, string playerName)
        {
		    var model = _cashgameModelService.GetActionChartJsonModel(slug, dateStr, playerName);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        [AuthorizeOwnPlayer]
        public ActionResult Buyin(string slug, string playerName)
        {
            var model = _cashgameModelService.GetBuyinModel(slug, playerName);
            return View("Buyin/Buyin", model);
		}

        [HttpPost]
        [AuthorizeOwnPlayer]
        public ActionResult Buyin(string slug, string playerName, BuyinPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetBuyinCommand(slug, playerName, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _cashgameModelService.GetBuyinModel(slug, playerName, postModel);
            return View("Buyin/Buyin", model);
		}

        [AuthorizeOwnPlayer]
        public ActionResult Report(string slug, string playerName)
        {
            var model = _cashgameModelService.GetReportModel(slug);
            return View("Report/Report", model);
		}

        [HttpPost]
        [AuthorizeOwnPlayer]
        public ActionResult Report(string slug, string playerName, ReportPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetReportCommand(slug, playerName, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _cashgameModelService.GetReportModel(slug, postModel);
            return View("Report/Report", model);
		}

        [AuthorizeManager]
        public ActionResult EditCheckpoint(string slug, string dateStr, string playerName, int checkpointId)
        {
            var model = _cashgameModelService.GetEditCheckpointModel(slug, dateStr, playerName, checkpointId);
            return View("Checkpoints/Edit", model);
        }

        [HttpPost]
        [AuthorizeManager]
        public ActionResult EditCheckpoint(string slug, string dateStr, string playerName, int checkpointId, EditCheckpointPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetEditCheckpointCommand(slug, dateStr, checkpointId, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetCashgameActionUrl(slug, dateStr, playerName));
            }
            AddModelErrors(command.Errors);
            var model = _cashgameModelService.GetEditCheckpointModel(slug, dateStr, playerName, checkpointId, postModel);
            return View("Checkpoints/Edit", model);
        }

        [AuthorizeManager]
        public ActionResult DeleteCheckpoint(string slug, string dateStr, string playerName, int checkpointId)
        {
            var command = _cashgameCommandProvider.GetDeleteCheckpointCommand(slug, dateStr, checkpointId);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
            }
            var actionsUrl = _urlProvider.GetCashgameActionUrl(slug, dateStr, playerName);
            return Redirect(actionsUrl);
		}
        
        [AuthorizeOwnPlayer]
        public ActionResult Cashout(string slug, string playerName)
        {
            var model = _cashgameModelService.GetCashoutModel(slug);
            return View("Cashout/Cashout", model);
		}

        [HttpPost]
        [AuthorizeOwnPlayer]
        public ActionResult Cashout(string slug, string playerName, CashoutPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetCashoutCommand(slug, playerName, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
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
            return Redirect(_urlProvider.GetCashgameIndexUrl(slug));
		}

        [AuthorizeManager]
        public ActionResult Delete(string slug, string dateStr)
        {
            var command = _cashgameCommandProvider.GetDeleteCommand(slug, dateStr);
            command.Execute();
            return Redirect(_urlProvider.GetCashgameIndexUrl(slug));
		}
	}
}