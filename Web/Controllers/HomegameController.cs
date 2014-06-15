using System.Web.Mvc;
using Application.Urls;
using Web.Commands.HomegameCommands;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelServices;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Models.UrlModels;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class HomegameController : ControllerBase
    {
	    private readonly IHomegameCommandProvider _homegameCommandProvider;
	    private readonly IHomegameModelService _homegameModelService;
	    private readonly IBunchListPageBuilder _bunchListPageBuilder;
	    private readonly IHomegameDetailsPageBuilder _homegameDetailsPageBuilder;

	    public HomegameController(
            IHomegameCommandProvider homegameCommandProvider,
            IHomegameModelService homegameModelService,
            IBunchListPageBuilder bunchListPageBuilder,
            IHomegameDetailsPageBuilder homegameDetailsPageBuilder)
	    {
	        _homegameCommandProvider = homegameCommandProvider;
	        _homegameModelService = homegameModelService;
	        _bunchListPageBuilder = bunchListPageBuilder;
	        _homegameDetailsPageBuilder = homegameDetailsPageBuilder;
	    }

        [AuthorizeAdmin]
	    public ActionResult List()
        {
            var model = _bunchListPageBuilder.Build();
			return View("HomegameList", model);
		}

        [AuthorizePlayer]
        public ActionResult Details(string slug)
        {
            var model = _homegameDetailsPageBuilder.Build(slug);
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
                return Redirect(new AddHomegameConfirmationUrl().Relative);
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
                return Redirect(new HomegameDetailsUrl(slug).Relative);
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
                return Redirect(new JoinHomegameConfirmationUrl(slug).Relative);
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