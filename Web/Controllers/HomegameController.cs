using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.UrlModels;

namespace Web.Controllers{

	public class HomegameController : Controller
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly IHomegameModelMapper _modelMapper;
	    private readonly IAddHomegamePageModelFactory _addHomegamePageModelFactory;
        private readonly IHomegameEditPageModelFactory _HomegameEditPageModelFactory;
	    private readonly IAddHomegameConfirmationPageModelFactory _addHomegameConfirmationPageModelFactory;
	    private readonly ISlugGenerator _slugGenerator;
	    private readonly IHomegameListingPageModelFactory _homegameListingPageModelFactory;
	    private readonly IHomegameDetailsPageModelFactory _homegameDetailsPageModelFactory;
	    private readonly IHomegameStorage _homegameStorage;
	    private readonly ICashgameRepository _cashgameRepository;

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
            IHomegameStorage homegameStorage,
            ICashgameRepository cashgameRepository)
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
	        _homegameStorage = homegameStorage;
	        _cashgameRepository = cashgameRepository;
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
            return ShowForm(model);
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
                    return new RedirectResult(new HomegameAddConfirmationUrlModel().Url);
                }
                else
                {
                    ModelState.AddModelError("homegame_exists", "The Homegame name is not available");
                }
			}
            var model = _addHomegamePageModelFactory.Create(_userContext.GetUser(), postModel);
			return ShowForm(model);
		}

        public ActionResult Created(){
			var model = _addHomegameConfirmationPageModelFactory.Create(_userContext.GetUser());
			return View("AddHomegameConfirmation", model);
		}

        public ActionResult Edit(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
            var runningGame = _cashgameRepository.GetRunning(homegame);
			var model = _HomegameEditPageModelFactory.Create(_userContext.GetUser(), homegame, runningGame);
			return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string gameName, HomegameEditPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			if(ModelState.IsValid)
			{
			    var postedHomegame = _modelMapper.GetHomegame(homegame, postModel);
				_homegameStorage.UpdateHomegame(postedHomegame);
				return new RedirectResult(new HomegameDetailsUrlModel(postedHomegame).Url);
			}
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _HomegameEditPageModelFactory.Create(_userContext.GetUser(), homegame, runningGame, postModel);
			return View("Edit/Edit", model);
		}

        /*
		private function getPostedHomegame(Homegame $homegame){
			$homegame.setDescription(request.getParamPost('description'));
			$homegame.setHouseRules(request.getParamPost('houserules'));
			$currencySymbol = request.getParamPost('currencysymbol');
			$currencyLayout = request.getParamPost('currencylayout');
			$currency = new CurrencySettings($currencySymbol, $currencyLayout);
			$homegame.setCurrency($currency);
			$timezoneName = request.getParamPost('timezone');
			if($timezoneName != null){
				$homegame.setTimezone(new DateTimeZone($timezoneName));
			}
			$homegame.setDefaultBuyin(request.getParamPost('defaultbuyin'));
			$homegame.cashgamesEnabled = request.getParamPost('cashgames') != null;
			$homegame.tournamentsEnabled = request.getParamPost('tournaments') != null;
			$homegame.videosEnabled = request.getParamPost('videos') != null;
			return $homegame;
		}
        */

        private ActionResult ShowForm(AddHomegamePageModel model = null){
			return View("AddHomegame", model);
		}

        private bool HomegameExists(AddHomegamePostModel postModel)
        {
            var slug = _slugGenerator.GetSlug(postModel.DisplayName);
            var homegame = _homegameRepository.GetByName(slug);
            return homegame != null;
        }
		
    }
}