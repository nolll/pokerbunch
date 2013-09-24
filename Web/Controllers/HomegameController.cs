using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Models.UrlModels;

namespace Web.Controllers{

	public class HomegameController : Controller
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly IHomegameModelMapper _modelMapper;
	    private readonly IAddHomegamePageModelFactory _addHomegamePageModelFactory;
	    private readonly IAddHomegameConfirmationPageModelFactory _addHomegameConfirmationPageModelFactory;
	    private readonly ISlugGenerator _slugGenerator;
	    private readonly IHomegameListingPageModelFactory _homegameListingPageModelFactory;
	    private readonly IHomegameDetailsPageModelFactory _homegameDetailsPageModelFactory;
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IHomegameEditPageModelFactory _homegameEditPageModelFactory;
	    private readonly IJoinHomegamePageModelFactory _joinHomegamePageModelFactory;
	    private readonly IJoinHomegameConfirmationPageModelFactory _joinHomegameConfirmationPageModelFactory;
	    private readonly IInvitationCodeCreator _invitationCodeCreator;

	    public HomegameController(
            IUserContext userContext,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            IHomegameModelMapper modelMapper,
            IAddHomegamePageModelFactory addHomegamePageModelFactory,
            IAddHomegameConfirmationPageModelFactory addHomegameConfirmationPageModelFactory,
            ISlugGenerator slugGenerator,
            IHomegameListingPageModelFactory homegameListingPageModelFactory,
            IHomegameDetailsPageModelFactory homegameDetailsPageModelFactory,
            ICashgameRepository cashgameRepository,
            IHomegameEditPageModelFactory homegameEditPageModelFactory,
            IJoinHomegamePageModelFactory joinHomegamePageModelFactory,
            IJoinHomegameConfirmationPageModelFactory joinHomegameConfirmationPageModelFactory,
            IInvitationCodeCreator invitationCodeCreator)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _playerRepository = playerRepository;
	        _modelMapper = modelMapper;
	        _addHomegamePageModelFactory = addHomegamePageModelFactory;
	        _addHomegameConfirmationPageModelFactory = addHomegameConfirmationPageModelFactory;
	        _slugGenerator = slugGenerator;
	        _homegameListingPageModelFactory = homegameListingPageModelFactory;
	        _homegameDetailsPageModelFactory = homegameDetailsPageModelFactory;
	        _cashgameRepository = cashgameRepository;
	        _homegameEditPageModelFactory = homegameEditPageModelFactory;
	        _joinHomegamePageModelFactory = joinHomegamePageModelFactory;
	        _joinHomegameConfirmationPageModelFactory = joinHomegameConfirmationPageModelFactory;
	        _invitationCodeCreator = invitationCodeCreator;
	    }

	    public ActionResult Listing(){
			_userContext.RequireAdmin();
			var homegames = _homegameRepository.GetAll();
			var model = _homegameListingPageModelFactory.Create(_userContext.GetUser(), homegames);
			return View("HomegameListing", model);
		}

        public ActionResult Details(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var isInManagerMode = _userContext.IsInRole(homegame, Role.Manager);
			var model = _homegameDetailsPageModelFactory.Create(_userContext.GetUser(), homegame, isInManagerMode);
			return View("HomegameDetails", model);
		}

        public ActionResult Add()
        {
            _userContext.RequireUser();
            var model = _addHomegamePageModelFactory.Create(_userContext.GetUser());
            return View("AddHomegame", model);
        }

        [HttpPost]
        public ActionResult Add(AddHomegamePostModel postModel){
			_userContext.RequireUser();
			if(ModelState.IsValid){
                if (!HomegameExists(postModel))
                {
                    var homegame = _modelMapper.GetHomegame(postModel);
                    homegame = _homegameRepository.AddHomegame(homegame);
                    var user = _userContext.GetUser();
                    _playerRepository.AddPlayerWithUser(homegame, user, Role.Manager);
                    return Redirect(new HomegameAddConfirmationUrlModel().Url);
                }
                else
                {
                    ModelState.AddModelError("homegame_exists", "The Homegame name is not available");
                }
			}
            var model = _addHomegamePageModelFactory.Create(_userContext.GetUser(), postModel);
			return View("AddHomegame", model);
		}

        public ActionResult Created(){
			var model = _addHomegameConfirmationPageModelFactory.Create(_userContext.GetUser());
			return View("AddHomegameConfirmation", model);
		}

        public ActionResult Edit(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
            var runningGame = _cashgameRepository.GetRunning(homegame);
			var model = _homegameEditPageModelFactory.Create(_userContext.GetUser(), homegame, runningGame);
			return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string gameName, HomegameEditPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			if(ModelState.IsValid)
			{
			    var postedHomegame = _modelMapper.GetHomegame(homegame, postModel);
				_homegameRepository.SaveHomegame(postedHomegame);
                return Redirect(new HomegameDetailsUrlModel(postedHomegame).Url);
			}
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _homegameEditPageModelFactory.Create(_userContext.GetUser(), homegame, runningGame, postModel);
			return View("Edit/Edit", model);
		}

        public ActionResult Join(string gameName){
			_userContext.RequireUser();
            var model = _joinHomegamePageModelFactory.Create(_userContext.GetUser());
			return View("Join/Join", model);
		}

        [HttpPost]
		public ActionResult Join(string gameName, JoinHomegamePostModel postModel){
			_userContext.RequireUser();
			var homegame = _homegameRepository.GetByName(gameName);
            if (ModelState.IsValid)
            {
                var player = GetMatchedPlayer(homegame, postModel.Code);
                if(player != null && player.UserName == null){
				    var user = _userContext.GetUser();
				    _playerRepository.JoinHomegame(player, homegame, user);
				    return Redirect(new HomegameJoinConfirmationUrlModel(homegame).Url);
			    }
            }
            ModelState.AddModelError("joincode", "That code didn't work. Please check for errors and try again");
			var model = _joinHomegamePageModelFactory.Create(_userContext.GetUser());
			return View("Join/Join", model);
		}

		public ActionResult Joined(string gameName)
		{
		    var model = _joinHomegameConfirmationPageModelFactory.Create(_userContext.GetUser());
			return View("Join/Confirmation", model);
		}

		private Player GetMatchedPlayer(Homegame homegame, string postedCode){
			var players = _playerRepository.GetAll(homegame);
			foreach (var player in players) {
				var code = _invitationCodeCreator.GetCode(player);
				if(code == postedCode){
					return player;
				}
			}
			return null;
		}

        private bool HomegameExists(AddHomegamePostModel postModel)
        {
            var slug = _slugGenerator.GetSlug(postModel.DisplayName);
            var homegame = _homegameRepository.GetByName(slug);
            return homegame != null;
        }
		
    }
}