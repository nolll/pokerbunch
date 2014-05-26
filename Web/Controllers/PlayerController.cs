using System.Web.Mvc;
using Application.Services;
using Web.Commands.PlayerCommands;
using Web.ModelFactories.PlayerModelFactories;
using Web.ModelServices;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class PlayerController : ControllerBase
    {
	    private readonly IPlayerModelService _playerModelService;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IPlayerCommandProvider _playerCommandProvider;
        private readonly IPlayerListPageBuilder _playerListPageBuilder;

        public PlayerController(
            IPlayerModelService playerModelService,
            IUrlProvider urlProvider,
            IPlayerCommandProvider playerCommandProvider,
            IPlayerListPageBuilder playerListPageBuilder)
	    {
	        _playerModelService = playerModelService;
	        _urlProvider = urlProvider;
	        _playerCommandProvider = playerCommandProvider;
	        _playerListPageBuilder = playerListPageBuilder;
	    }

        [AuthorizePlayer]
	    public ActionResult Index(string slug)
        {
            var model = _playerListPageBuilder.Build(slug);
			return View("List", model);
		}

        [AuthorizePlayer]
        public ActionResult Details(string slug, int playerId)
        {
            var model = _playerModelService.GetDetailsModel(slug, playerId);
			return View("Details", model);
		}

        [AuthorizeManager]
        public ActionResult Add(string slug)
        {
            var model = _playerModelService.GetAddModel(slug);
            return View("Add", model);
		}

        [HttpPost]
        [AuthorizeManager]
        public ActionResult Add(string slug, AddPlayerPostModel postModel)
        {
            var command = _playerCommandProvider.GetAddCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerAddConfirmationUrl(slug));
            }
            AddModelErrors(command.Errors);
			var model = _playerModelService.GetAddModel(slug, postModel);
			return View("Add", model);
		}

        public ActionResult Created(string slug)
        {
            var model = _playerModelService.GetAddConfirmationModel(slug);
			return View("AddConfirmation", model);
		}

        [AuthorizeManager]
        public ActionResult Delete(string slug, int playerId)
        {
            var command = _playerCommandProvider.GetDeleteCommand(slug, playerId);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerIndexUrl(slug));
            }
            return Redirect(_urlProvider.GetPlayerDetailsUrl(slug, playerId));
		}

        [AuthorizeManager]
        public ActionResult Invite(string slug, int playerId)
        {
            var model = _playerModelService.GetInviteModel(slug);
            return View("Invite", model);
		}

        [HttpPost]
        [AuthorizeManager]
        public ActionResult Invite(string slug, int playerId, InvitePlayerPostModel postModel)
        {
            var command = _playerCommandProvider.GetInviteCommand(slug, playerId, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerInviteConfirmationUrl(slug, playerId));
            }
            AddModelErrors(command.Errors);
            var model = _playerModelService.GetInviteModel(slug, postModel);
            return View("Invite", model);
		}

        public ActionResult Invited(string slug, int playerId)
        {
		    var model = _playerModelService.GetInviteConfirmationModel(slug);
			return View("InviteConfirmation", model);
		}
    }

}