using System.Web.Mvc;
using Application.Urls;
using Web.Commands.PlayerCommands;
using Web.ModelFactories.PlayerModelFactories;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class PlayerController : ControllerBase
    {
	    private readonly IPlayerCommandProvider _playerCommandProvider;
        private readonly IPlayerListPageBuilder _playerListPageBuilder;
        private readonly IPlayerDetailsPageBuilder _playerDetailsPageBuilder;
        private readonly IAddPlayerPageBuilder _addPlayerPageBuilder;
        private readonly IAddPlayerConfirmationPageBuilder _addPlayerConfirmationPageBuilder;
        private readonly IInvitePlayerPageBuilder _invitePlayerPageBuilder;
        private readonly IInvitePlayerConfirmationPageBuilder _invitePlayerConfirmationPageBuilder;

        public PlayerController(
            IPlayerCommandProvider playerCommandProvider,
            IPlayerListPageBuilder playerListPageBuilder,
            IPlayerDetailsPageBuilder playerDetailsPageBuilder,
            IAddPlayerPageBuilder addPlayerPageBuilder,
            IAddPlayerConfirmationPageBuilder addPlayerConfirmationPageBuilder,
            IInvitePlayerPageBuilder invitePlayerPageBuilder,
            IInvitePlayerConfirmationPageBuilder invitePlayerConfirmationPageBuilder)
	    {
	        _playerCommandProvider = playerCommandProvider;
	        _playerListPageBuilder = playerListPageBuilder;
            _playerDetailsPageBuilder = playerDetailsPageBuilder;
            _addPlayerPageBuilder = addPlayerPageBuilder;
            _addPlayerConfirmationPageBuilder = addPlayerConfirmationPageBuilder;
            _invitePlayerPageBuilder = invitePlayerPageBuilder;
            _invitePlayerConfirmationPageBuilder = invitePlayerConfirmationPageBuilder;
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
            var model = _playerDetailsPageBuilder.Build(slug, playerId);
			return View("Details", model);
		}

        [AuthorizeManager]
        public ActionResult Add(string slug)
        {
            var model = _addPlayerPageBuilder.Build(slug);
            return View("Add", model);
		}

        [HttpPost]
        [AuthorizeManager]
        public ActionResult Add(string slug, AddPlayerPostModel postModel)
        {
            var command = _playerCommandProvider.GetAddCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(new AddPlayerConfirmationUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _addPlayerPageBuilder.Build(slug, postModel);
			return View("Add", model);
		}

        public ActionResult Created(string slug)
        {
            var model = _addPlayerConfirmationPageBuilder.Build(slug);
			return View("AddConfirmation", model);
		}

        [AuthorizeManager]
        public ActionResult Delete(string slug, int playerId)
        {
            var command = _playerCommandProvider.GetDeleteCommand(slug, playerId);
            if (command.Execute())
            {
                return Redirect(new PlayerIndexUrl(slug).Relative);
            }
            return Redirect(new PlayerDetailsUrl(slug, playerId).Relative);
		}

        [AuthorizeManager]
        public ActionResult Invite(string slug, int playerId)
        {
            var model = _invitePlayerPageBuilder.Build(slug);
            return View("Invite", model);
		}

        [HttpPost]
        [AuthorizeManager]
        public ActionResult Invite(string slug, int playerId, InvitePlayerPostModel postModel)
        {
            var command = _playerCommandProvider.GetInviteCommand(slug, playerId, postModel);
            if (command.Execute())
            {
                return Redirect(new InvitePlayerConfirmationUrl(slug, playerId).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _invitePlayerPageBuilder.Build(slug, postModel);
            return View("Invite", model);
		}

        public ActionResult Invited(string slug, int playerId)
        {
		    var model = _invitePlayerConfirmationPageBuilder.Build(slug);
			return View("InviteConfirmation", model);
		}
    }

}