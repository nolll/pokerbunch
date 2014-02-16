using System.Web.Mvc;
using Application.Exceptions;
using Application.Services;
using Web.Commands.CashgameCommands;
using Web.ModelServices;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Report;

namespace Web.Controllers
{
	public class CashgameController : ControllerBase
    {
	    private readonly IAuthentication _authentication;
	    private readonly IAuthorization _authorization;
	    private readonly IUrlProvider _urlProvider;
	    private readonly ICashgameService _cashgameService;
	    private readonly ICashgameCommandProvider _cashgameCommandProvider;
	    private readonly ICashgameModelService _cashgameModelService;

	    public CashgameController(
            IAuthentication authentication, 
            IAuthorization authorization,
            IUrlProvider urlProvider,
            ICashgameService cashgameService,
            ICashgameCommandProvider cashgameCommandProvider,
            ICashgameModelService cashgameModelService)
	    {
	        _authentication = authentication;
	        _authorization = authorization;
	        _urlProvider = urlProvider;
	        _cashgameService = cashgameService;
	        _cashgameCommandProvider = cashgameCommandProvider;
	        _cashgameModelService = cashgameModelService;
	    }

	    public ActionResult Index(string slug)
	    {
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var url = _cashgameModelService.GetIndexUrl(slug);
            return Redirect(url);
		}

        public ActionResult Matrix(string slug, int? year = null)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetMatrixModel(slug, year);
			return View("Matrix/MatrixPage", model);
		}

        public ActionResult Toplist(string slug, int? year = null)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetToplistModel(slug, year);
            return View("Toplist/ToplistPage", model);
		}

	    public ActionResult Details(string slug, string dateStr)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetDetailsModel(slug, dateStr);
			return View("Details/DetailsPage", model);
		}

        public ActionResult DetailsChartJson(string slug, string dateStr)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetDetailsChartJsonModel(slug, dateStr);
		    return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Facts(string slug, int? year = null)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetFactsModel(slug, year);
			return View("Facts/FactsPage", model);
		}

        public ActionResult Add(string slug)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetAddModel(slug);
            return View("Add/Add", model);
		}

        [HttpPost]
        public ActionResult Add(string slug, AddCashgamePostModel postModel)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var command = _cashgameCommandProvider.GetAddCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _cashgameModelService.GetAddModel(slug, postModel);
            return View("Add/Add", model);
		}

        public ActionResult Edit(string slug, string dateStr)
        {
            _authentication.RequireUser();
            _authorization.RequireManager(slug);
            var model = _cashgameModelService.GetEditModel(slug, dateStr);
			return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string slug, string dateStr, CashgameEditPostModel postModel)
        {
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var command = _cashgameCommandProvider.GetEditCommand(slug, dateStr, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetCashgameDetailsUrl(slug, dateStr));
            }
            AddModelErrors(command.Errors);
            var model = _cashgameModelService.GetEditModel(slug, dateStr, postModel);
            return View("Edit/Edit", model);
		}

        public ActionResult Running(string slug)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            if(!_cashgameService.CashgameIsRunning(slug))
            {
                return Redirect(_urlProvider.GetCashgameIndexUrl(slug));
			}
			var model = _cashgameModelService.GetRunningModel(slug);
			return View("Running/RunningPage", model);
		}

        public ActionResult List(string slug, int? year = null)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetListModel(slug, year);
            return View("List/List", model);
		}

        public ActionResult Chart(string slug, int? year = null)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetChartModel(slug, year);
            return View("Chart/Chart", model);
		}

        public JsonResult ChartJson(string slug, int? year = null)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetChartJsonModel(slug, year);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Action(string slug, string dateStr, string playerName)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetActionModel(slug, dateStr, playerName);
			return View("Action/Action", model);
		}

		public JsonResult ActionChartJson(string slug, string dateStr, string playerName)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
		    var model = _cashgameModelService.GetActionChartJsonModel(slug, dateStr, playerName);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Buyin(string slug, string playerName)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            if (!_authorization.CanActAsPlayer(slug, playerName))
            {
                throw new AccessDeniedException();
            } 
            var model = _cashgameModelService.GetBuyinModel(slug, playerName);
            return View("Buyin/Buyin", model);
		}

        [HttpPost]
        public ActionResult Buyin(string slug, string playerName, BuyinPostModel postModel)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            if (!_authorization.CanActAsPlayer(slug, playerName))
            {
                throw new AccessDeniedException();
            }
            var command = _cashgameCommandProvider.GetBuyinCommand(slug, playerName, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _cashgameModelService.GetBuyinModel(slug, playerName, postModel);
            return View("Buyin/Buyin", model);
		}

        public ActionResult Report(string slug, string playerName)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            if (!_authorization.CanActAsPlayer(slug, playerName))
            {
                throw new AccessDeniedException();
            }
            var model = _cashgameModelService.GetReportModel(slug);
            return View("Report/Report", model);
		}

        [HttpPost]
        public ActionResult Report(string slug, string playerName, ReportPostModel postModel)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            if (!_authorization.CanActAsPlayer(slug, playerName))
            {
                throw new AccessDeniedException();
            }
            var command = _cashgameCommandProvider.GetReportCommand(slug, playerName, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _cashgameModelService.GetReportModel(slug, postModel);
            return View("Report/Report", model);
		}

        public ActionResult DeleteCheckpoint(string slug, string dateStr, string playerName, int checkpointId)
        {
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var command = _cashgameCommandProvider.GetDeleteCheckpointCommand(slug, dateStr, checkpointId);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
            }
            var actionsUrl = _urlProvider.GetCashgameActionUrl(slug, dateStr, playerName);
            return Redirect(actionsUrl);
		}

        public ActionResult Cashout(string slug, string playerName)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            if (!_authorization.CanActAsPlayer(slug, playerName))
            {
                throw new AccessDeniedException();
            }
            var model = _cashgameModelService.GetCashoutModel(slug);
            return View("Cashout/Cashout", model);
		}

        [HttpPost]
        public ActionResult Cashout(string slug, string playerName, CashoutPostModel postModel)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            if (!_authorization.CanActAsPlayer(slug, playerName))
            {
                throw new AccessDeniedException();
            }
            var command = _cashgameCommandProvider.GetCashoutCommand(slug, playerName, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetRunningCashgameUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _cashgameModelService.GetCashoutModel(slug, postModel);
            return View("Cashout/Cashout", model);
		}

        public ActionResult End(string slug)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _cashgameModelService.GetEndGameModel(slug);
			return View("End/End", model);
		}

        [HttpPost]
		public ActionResult End(string slug, EndPageModel postModel)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var command = _cashgameCommandProvider.GetEndGameCommand(slug);
            command.Execute();
            return Redirect(_urlProvider.GetCashgameIndexUrl(slug));
		}

        public ActionResult Delete(string slug, string dateStr)
        {
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var command = _cashgameCommandProvider.GetDeleteCommand(slug, dateStr);
            command.Execute();
            return Redirect(_urlProvider.GetCashgameIndexUrl(slug));
		}
	}
}