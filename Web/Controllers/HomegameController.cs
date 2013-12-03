using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.Commands.HomegameCommands;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;

namespace Web.Controllers
{
	public class HomegameController : ControllerBase
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IAddHomegamePageModelFactory _addHomegamePageModelFactory;
	    private readonly IAddHomegameConfirmationPageModelFactory _addHomegameConfirmationPageModelFactory;
	    private readonly IHomegameListPageModelFactory _homegameListPageModelFactory;
	    private readonly IHomegameDetailsPageModelFactory _homegameDetailsPageModelFactory;
	    private readonly IHomegameEditPageModelFactory _homegameEditPageModelFactory;
	    private readonly IJoinHomegamePageModelFactory _joinHomegamePageModelFactory;
	    private readonly IJoinHomegameConfirmationPageModelFactory _joinHomegameConfirmationPageModelFactory;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IHomegameCommandProvider _homegameCommandProvider;

	    public HomegameController(
            IUserContext userContext,
            IHomegameRepository homegameRepository,
            IAddHomegamePageModelFactory addHomegamePageModelFactory,
            IAddHomegameConfirmationPageModelFactory addHomegameConfirmationPageModelFactory,
            IHomegameListPageModelFactory homegameListPageModelFactory,
            IHomegameDetailsPageModelFactory homegameDetailsPageModelFactory,
            IHomegameEditPageModelFactory homegameEditPageModelFactory,
            IJoinHomegamePageModelFactory joinHomegamePageModelFactory,
            IJoinHomegameConfirmationPageModelFactory joinHomegameConfirmationPageModelFactory,
            IUrlProvider urlProvider,
            IHomegameCommandProvider homegameCommandProvider)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _addHomegamePageModelFactory = addHomegamePageModelFactory;
	        _addHomegameConfirmationPageModelFactory = addHomegameConfirmationPageModelFactory;
	        _homegameListPageModelFactory = homegameListPageModelFactory;
	        _homegameDetailsPageModelFactory = homegameDetailsPageModelFactory;
	        _homegameEditPageModelFactory = homegameEditPageModelFactory;
	        _joinHomegamePageModelFactory = joinHomegamePageModelFactory;
	        _joinHomegameConfirmationPageModelFactory = joinHomegameConfirmationPageModelFactory;
	        _urlProvider = urlProvider;
	        _homegameCommandProvider = homegameCommandProvider;
	    }

	    public ActionResult List()
        {
			_userContext.RequireAdmin();
			var homegames = _homegameRepository.GetList();
			var model = _homegameListPageModelFactory.Create(_userContext.GetUser(), homegames);
			return View("HomegameList", model);
		}

        public ActionResult Details(string gameName)
        {
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
        public ActionResult Add(AddHomegamePostModel postModel)
        {
			_userContext.RequireUser();
            var command = _homegameCommandProvider.GetAddCommand(postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameAddConfirmationUrl());
            }
            AddModelErrors(command.Errors);
            var model = _addHomegamePageModelFactory.Create(_userContext.GetUser(), postModel);
            return View("AddHomegame", model);
		}

        public ActionResult Created()
        {
			var model = _addHomegameConfirmationPageModelFactory.Create(_userContext.GetUser());
			return View("AddHomegameConfirmation", model);
		}

        public ActionResult Edit(string gameName)
        {
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var model = _homegameEditPageModelFactory.Create(_userContext.GetUser(), homegame);
			return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string gameName, HomegameEditPostModel postModel)
        {
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
            var command = _homegameCommandProvider.GetEditCommand(homegame, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameDetailsUrl(homegame));
            }
            AddModelErrors(command.Errors);
            var model = _homegameEditPageModelFactory.Create(_userContext.GetUser(), homegame, postModel);
            return View("Edit/Edit", model);
		}

        public ActionResult Join(string gameName)
        {
			_userContext.RequireUser();
            var homegame = _homegameRepository.GetByName(gameName);
            var model = _joinHomegamePageModelFactory.Create(_userContext.GetUser(), homegame);
			return View("Join/Join", model);
		}

        [HttpPost]
		public ActionResult Join(string gameName, JoinHomegamePostModel postModel)
        {
			_userContext.RequireUser();
			var homegame = _homegameRepository.GetByName(gameName);
            var command = _homegameCommandProvider.GetJoinCommand(homegame, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameJoinConfirmationUrl(homegame));
            }
            AddModelErrors(command.Errors);
            var model = _joinHomegamePageModelFactory.Create(_userContext.GetUser(), homegame, postModel);
            return View("Join/Join", model);
		}

		public ActionResult Joined(string gameName)
		{
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
		    var model = _joinHomegameConfirmationPageModelFactory.Create(_userContext.GetUser(), homegame);
			return View("Join/Confirmation", model);
		}

    }
}