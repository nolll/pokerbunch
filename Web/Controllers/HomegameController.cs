using System.Web.Mvc;
using Application.Services;
using Core.Classes;
using Web.Commands.HomegameCommands;
using Web.ModelServices;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Security;
using Web.Services;

namespace Web.Controllers
{
	public class HomegameController : ControllerBase
    {
	    private readonly IAuthentication _authentication;
	    private readonly IAuthorization _authorization;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IHomegameCommandProvider _homegameCommandProvider;
	    private readonly IHomegameModelService _homegameModelService;

	    public HomegameController(
            IAuthentication authentication,
            IAuthorization authorization,
            IUrlProvider urlProvider,
            IHomegameCommandProvider homegameCommandProvider,
            IHomegameModelService homegameModelService)
	    {
	        _authentication = authentication;
	        _authorization = authorization;
	        _urlProvider = urlProvider;
	        _homegameCommandProvider = homegameCommandProvider;
	        _homegameModelService = homegameModelService;
	    }

        [AuthorizeRole(Role = Role.Admin)]
	    public ActionResult List()
        {
	        var model = _homegameModelService.GetListModel();
			return View("HomegameList", model);
		}

        public ActionResult Details(string slug)
        {
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _homegameModelService.GetDetailsModel(slug);
			return View("HomegameDetails", model);
		}

        public ActionResult Add()
        {
            _authentication.RequireUser();
            var model = _homegameModelService.GetAddModel();
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
            var model = _homegameModelService.GetAddModel(postModel);
            return View("AddHomegame", model);
		}

        public ActionResult Created()
        {
            var model = _homegameModelService.GetAddConfirmationModel();
			return View("AddHomegameConfirmation", model);
		}

        [AuthorizeRole(Role = Role.Manager)]
        public ActionResult Edit(string slug)
        {
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var model = _homegameModelService.GetEditModel(slug);
			return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string slug, HomegameEditPostModel postModel)
        {
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var command = _homegameCommandProvider.GetEditCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameDetailsUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _homegameModelService.GetEditModel(slug, postModel);
            return View("Edit/Edit", model);
		}

        public ActionResult Join(string slug)
        {
			_authentication.RequireUser();
            var model = _homegameModelService.GetJoinModel(slug);
			return View("Join/Join", model);
		}

        [HttpPost]
		public ActionResult Join(string slug, JoinHomegamePostModel postModel)
        {
			_authentication.RequireUser();
            var command = _homegameCommandProvider.GetJoinCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameJoinConfirmationUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _homegameModelService.GetJoinModel(slug, postModel);
            return View("Join/Join", model);
		}

		public ActionResult Joined(string slug)
		{
            _authentication.RequireUser();
            _authorization.RequirePlayer(slug);
		    var model = _homegameModelService.GetJoinConfirmationModel(slug);
			return View("Join/Confirmation", model);
		}

    }
}