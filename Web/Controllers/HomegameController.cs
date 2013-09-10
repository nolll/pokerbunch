using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Details;
using Web.Models.HomegameModels.Listing;
using Web.Validators;
using app;

namespace Web.Controllers{

	public class HomegameController : Controller
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IHomegameValidatorFactory _homegameValidatorFactory;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly IHomegameModelMapper _modelMapper;

	    public HomegameController(
            IUserContext userContext,
            IHomegameRepository homegameRepository,
            IHomegameValidatorFactory homegameValidatorFactory,
            IPlayerRepository playerRepository,
            IHomegameModelMapper modelMapper)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _homegameValidatorFactory = homegameValidatorFactory;
	        _playerRepository = playerRepository;
	        _modelMapper = modelMapper;
	    }

	    public ActionResult Listing(){
			_userContext.RequireAdmin();
			var homegames = _homegameRepository.GetAll();
			var model = new HomegameListingPageModel(_userContext.GetUser(), homegames);
			return View("HomegameListing", model);
		}

        public ActionResult Details(string gamename){
			var homegame = _homegameRepository.GetByName(gamename);
			_userContext.RequirePlayer(homegame);
			var isInManagerMode = _userContext.IsInRole(homegame, Role.Manager);
			var model = new HomegameDetailsPageModel(_userContext.GetUser(), homegame, isInManagerMode);
			return View("HomegameDetails", model);
		}

        public ActionResult Add()
        {
            _userContext.RequireUser();
            var model = new HomegameAddModel(_userContext.GetUser());
            return ShowForm(model);
        }

        [HttpPost]
        public ActionResult Add(HomegameAddModel addHomegamePageModel){
			_userContext.RequireUser();
            var validator = _homegameValidatorFactory.GetAddHomegameValidator(addHomegamePageModel);
			if(validator.IsValid){
			    var homegame = _modelMapper.GetHomegame(addHomegamePageModel);
				homegame = _homegameRepository.AddHomegame(homegame);
				var user = _userContext.GetUser();
				_playerRepository.AddPlayerWithUser(homegame, user, Role.Manager);
                return new RedirectResult(new HomegameAddConfirmationUrlModel().Url);
			}
			return ShowForm(addHomegamePageModel, validator.GetErrors());
		}

		public ActionResult action_created(){
			var model = new HomegameAddConfirmationModel(_userContext.GetUser());
			return View("AddHomegameConfirmation", model);
		}

        /*
		private function getPostedHomegame(){
			$homegame = new Homegame();
			$homegame.setDisplayName(request.getParamPost('displayname'));
			$currencySymbol = request.getParamPost('currencysymbol');
			$currencyLayout = request.getParamPost('currencylayout');
			$currency = new CurrencySettings($currencySymbol, $currencyLayout);
			$homegame.setCurrency($currency);
			$timezoneName = request.getParamPost('timezone');
			if($timezoneName != null){
				$homegame.setTimezone(new DateTimeZone($timezoneName));
			}
			$homegame.setDescription(request.getParamPost('description'));
			$homegame.setSlug(slugGenerator.getSlug($homegame.getDisplayName()));
			return $homegame;
		}
        */

        private ActionResult ShowForm(HomegameAddModel model = null, List<string> validationErrors = null){
            if(validationErrors != null){
				model.SetValidationErrors(validationErrors);
			}
			return View("AddHomegame", model);
		}

    }
}