using System.Web.Mvc;
using Application.Services;
using Web.Commands.HomegameCommands;
using Web.ModelServices;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class HomegameController : ControllerBase
    {
	    private readonly IUrlProvider _urlProvider;
	    private readonly IHomegameCommandProvider _homegameCommandProvider;
	    private readonly IHomegameModelService _homegameModelService;

	    public HomegameController(
            IUrlProvider urlProvider,
            IHomegameCommandProvider homegameCommandProvider,
            IHomegameModelService homegameModelService)
	    {
	        _urlProvider = urlProvider;
	        _homegameCommandProvider = homegameCommandProvider;
	        _homegameModelService = homegameModelService;
	    }

        [AuthorizeAdmin]
	    public ActionResult List()
        {
	        var model = _homegameModelService.GetListModel();
			return View("HomegameList", model);
		}

        [AuthorizePlayer]
        public ActionResult Details(string slug)
        {
            var model = _homegameModelService.GetDetailsModel(slug);
			return View("HomegameDetails", model);
		}

        [Authorize]
        public ActionResult Add()
        {
            var model = _homegameModelService.GetAddModel();
            return View("AddHomegame", model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add(AddHomegamePostModel postModel)
        {
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

        [AuthorizeManager]
        public ActionResult Edit(string slug)
        {
            var model = _homegameModelService.GetEditModel(slug);
			return View("Edit/Edit", model);
		}

        [HttpPost]
        [AuthorizeManager]
        public ActionResult Edit(string slug, HomegameEditPostModel postModel)
        {
            var command = _homegameCommandProvider.GetEditCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameDetailsUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _homegameModelService.GetEditModel(slug, postModel);
            return View("Edit/Edit", model);
		}

        [Authorize]
        public ActionResult Join(string slug)
        {
            var model = _homegameModelService.GetJoinModel(slug);
			return View("Join/Join", model);
		}

        [HttpPost]
        [Authorize]
        public ActionResult Join(string slug, JoinHomegamePostModel postModel)
        {
            var command = _homegameCommandProvider.GetJoinCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetHomegameJoinConfirmationUrl(slug));
            }
            AddModelErrors(command.Errors);
            var model = _homegameModelService.GetJoinModel(slug, postModel);
            return View("Join/Join", model);
		}

        [AuthorizePlayer]
		public ActionResult Joined(string slug)
		{
		    var model = _homegameModelService.GetJoinConfirmationModel(slug);
			return View("Join/Confirmation", model);
		}

    }
}