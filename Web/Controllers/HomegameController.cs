using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Details;
using Web.Models.HomegameModels.Listing;
using app;

namespace Web.Controllers{

	public class HomegameController : Controller
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly IHomegameModelMapper _modelMapper;
	    private readonly IAddHomegamePageModelFactory _addHomegamePageModelFactory;
	    private readonly ISlugGenerator _slugGenerator;

	    public HomegameController(
            IUserContext userContext,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            IHomegameModelMapper modelMapper,
            IAddHomegamePageModelFactory addHomegamePageModelFactory,
            ISlugGenerator slugGenerator)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _playerRepository = playerRepository;
	        _modelMapper = modelMapper;
	        _addHomegamePageModelFactory = addHomegamePageModelFactory;
	        _slugGenerator = slugGenerator;
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

        private bool HomegameExists(AddHomegamePostModel postModel)
        {
            var slug = _slugGenerator.GetSlug(postModel.DisplayName);
            var homegame = _homegameRepository.GetByName(slug);
            return homegame != null;
        }

		public ActionResult Created(){
			var model = new HomegameAddConfirmationModel(_userContext.GetUser());
			return View("AddHomegameConfirmation", model);
		}

        private ActionResult ShowForm(AddHomegamePageModel model = null){
			return View("AddHomegame", model);
		}

    }
}