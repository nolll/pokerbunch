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
	    private readonly IAuthentication _authentication;
	    private readonly IAuthorization _authorization;
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
            IAuthentication authentication,
            IAuthorization authorization,
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
	        _authentication = authentication;
	        _authorization = authorization;
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
            _authentication.RequireUser();
            _authentication.RequireAdmin();
			var homegames = _homegameRepository.GetList();
			var model = _homegameListPageModelFactory.Create(_authentication.GetUser(), homegames);
			return View("HomegameList", model);
		}

        public ActionResult Details(string slug)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var isInManagerMode = _authorization.IsInRole(homegame, Role.Manager);
			var model = _homegameDetailsPageModelFactory.Create(_authentication.GetUser(), homegame, isInManagerMode);
			return View("HomegameDetails", model);
		}

        public ActionResult Add()
        {
            _authentication.RequireUser();
            var model = _addHomegamePageModelFactory.Create(_authentication.GetUser());
            return View("AddHomegame", model);
        }

        [HttpPost]
        public ActionResult Add(AddHomegamePostModel postModel)
        {
			_authentication.RequireUser();
            var command = _homegameCommandProvider.GetAddCommand(postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameAddConfirmationUrl());
            }
            AddModelErrors(command.Errors);
            var model = _addHomegamePageModelFactory.Create(_authentication.GetUser(), postModel);
            return View("AddHomegame", model);
		}

        public ActionResult Created()
        {
			var model = _addHomegameConfirmationPageModelFactory.Create(_authentication.GetUser());
			return View("AddHomegameConfirmation", model);
		}

        public ActionResult Edit(string slug)
        {
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var model = _homegameEditPageModelFactory.Create(_authentication.GetUser(), homegame);
			return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string slug, HomegameEditPostModel postModel)
        {
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var command = _homegameCommandProvider.GetEditCommand(homegame, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameDetailsUrl(homegame));
            }
            AddModelErrors(command.Errors);
            var model = _homegameEditPageModelFactory.Create(_authentication.GetUser(), homegame, postModel);
            return View("Edit/Edit", model);
		}

        public ActionResult Join(string slug)
        {
			_authentication.RequireUser();
            var homegame = _homegameRepository.GetByName(slug);
            var model = _joinHomegamePageModelFactory.Create(_authentication.GetUser(), homegame);
			return View("Join/Join", model);
		}

        [HttpPost]
		public ActionResult Join(string slug, JoinHomegamePostModel postModel)
        {
			_authentication.RequireUser();
			var homegame = _homegameRepository.GetByName(slug);
            var command = _homegameCommandProvider.GetJoinCommand(homegame, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameJoinConfirmationUrl(homegame));
            }
            AddModelErrors(command.Errors);
            var model = _joinHomegamePageModelFactory.Create(_authentication.GetUser(), homegame, postModel);
            return View("Join/Join", model);
		}

		public ActionResult Joined(string slug)
		{
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var model = _joinHomegameConfirmationPageModelFactory.Create(_authentication.GetUser(), homegame);
			return View("Join/Confirmation", model);
		}

    }
}