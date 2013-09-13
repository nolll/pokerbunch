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
	    private readonly IAddHomegamePageModelFactory _addHomegamePageModelFactory;

	    public HomegameController(
            IUserContext userContext,
            IHomegameRepository homegameRepository,
            IHomegameValidatorFactory homegameValidatorFactory,
            IPlayerRepository playerRepository,
            IHomegameModelMapper modelMapper,
            IAddHomegamePageModelFactory addHomegamePageModelFactory)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _homegameValidatorFactory = homegameValidatorFactory;
	        _playerRepository = playerRepository;
	        _modelMapper = modelMapper;
	        _addHomegamePageModelFactory = addHomegamePageModelFactory;
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
            var model = _addHomegamePageModelFactory.Create(_userContext.GetUser());
            return ShowForm(model);
        }

        [HttpPost]
        public ActionResult Add(AddHomegamePageModel addHomegamePageModel){
			_userContext.RequireUser();
            var validator = _homegameValidatorFactory.GetAddHomegameValidator(addHomegamePageModel);
			if(validator.IsValid){
			    var homegame = _modelMapper.GetHomegame(addHomegamePageModel);
				homegame = _homegameRepository.AddHomegame(homegame);
				var user = _userContext.GetUser();
				_playerRepository.AddPlayerWithUser(homegame, user, Role.Manager);
                return new RedirectResult(new HomegameAddConfirmationUrlModel().Url);
			}
            var model = _addHomegamePageModelFactory.ReBuild(_userContext.GetUser(), addHomegamePageModel);
			return ShowForm(model, validator.GetErrors());
		}

		public ActionResult Created(){
			var model = new HomegameAddConfirmationModel(_userContext.GetUser());
			return View("AddHomegameConfirmation", model);
		}

        private ActionResult ShowForm(AddHomegamePageModel model = null, List<string> validationErrors = null){
            if(validationErrors != null){
				model.SetValidationErrors(validationErrors);
			}
			return View("AddHomegame", model);
		}

    }
}