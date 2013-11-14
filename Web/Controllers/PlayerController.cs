using System.Web.Mvc;
using Core.Repositories;
using Core.Services;
using Web.ModelServices;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;

namespace Web.Controllers{

	public class PlayerController : Controller
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IInvitationSender _invitationSender;
	    private readonly IPlayerModelService _playerModelService;
	    private readonly IUrlProvider _urlProvider;

	    public PlayerController(
            IUserContext userContext,
			IHomegameRepository homegameRepository,
			IPlayerRepository playerRepository,
			ICashgameRepository cashgameRepository,
            IInvitationSender invitationSender,
            IPlayerModelService playerModelService,
            IUrlProvider urlProvider)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _playerRepository = playerRepository;
	        _cashgameRepository = cashgameRepository;
	        _invitationSender = invitationSender;
	        _playerModelService = playerModelService;
	        _urlProvider = urlProvider;
	    }

	    public ActionResult Index(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
	        var model = _playerModelService.GetListingModel(homegame);
			return View("Listing", model);
		}

        public ActionResult Details(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var model = _playerModelService.GetDetailsModel(homegame, name);
			return View("Details", model);
		}

        public ActionResult Add(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
            var model = _playerModelService.GetAddModel(homegame);
            return View("Add", model);
		}

        [HttpPost]
        public ActionResult Add(string gameName, AddPlayerPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			if(ModelState.IsValid)
			{
			    var existingPlayer = _playerRepository.GetByName(homegame, postModel.Name);
                if (existingPlayer == null)
                {
                    _playerRepository.AddPlayer(homegame, postModel.Name);
                    return Redirect(_urlProvider.GetPlayerAddConfirmationUrl(homegame));
                }
                ModelState.AddModelError("player_exists", "The Display Name is in use by someone else");
			}
            var model = _playerModelService.GetAddModel(homegame, postModel);
			return View("Add", model);
		}

        public ActionResult Created(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
            var model = _playerModelService.GetAddConfirmationModel(homegame);
			return View("AddConfirmation", model);
		}

		public ActionResult Delete(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var player = _playerRepository.GetByName(homegame, name);
			var hasPlayed = _cashgameRepository.HasPlayed(player);
			if(hasPlayed){
				return Redirect(_urlProvider.GetPlayerDetailsUrl(homegame, player));
			}
			_playerRepository.DeletePlayer(homegame, player);
			return Redirect(_urlProvider.GetPlayerIndexUrl(homegame));
		}

        public ActionResult Invite(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
            var model = _playerModelService.GetInviteModel(homegame);
            return View("Invite", model);
		}

        [HttpPost]
		public ActionResult Invite(string gameName, string name, InvitePlayerPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var player = _playerRepository.GetByName(homegame, name);
			if(ModelState.IsValid){
				_invitationSender.Send(homegame, player, postModel.Email);
				return Redirect(_urlProvider.GetPlayerInviteConfirmationUrl(homegame, player));
			}
            var model = _playerModelService.GetInviteModel(homegame);
            return View("Invite", model);
		}

		public ActionResult Invited(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
		    var model = _playerModelService.GetInviteConfirmationModel(homegame);
			return View("InviteConfirmation", model);
		}

    }

}