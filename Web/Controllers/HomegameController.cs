using System.Web.Mvc;
using Application.Urls;
using Web.Commands.HomegameCommands;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class HomegameController : ControllerBase
    {
	    private readonly IHomegameCommandProvider _homegameCommandProvider;
	    private readonly IBunchListPageBuilder _bunchListPageBuilder;
	    private readonly IHomegameDetailsPageBuilder _homegameDetailsPageBuilder;
	    private readonly IAddHomegamePageBuilder _addHomegamePageBuilder;
	    private readonly IAddHomegameConfirmationPageBuilder _addHomegameConfirmationPageBuilder;
	    private readonly IEditHomegamePageBuilder _editHomegamePageBuilder;
	    private readonly IJoinHomegamePageBuilder _joinHomegamePageBuilder;
	    private readonly IJoinHomegameConfirmationPageBuilder _joinHomegameConfirmationPageBuilder;

	    public HomegameController(
            IHomegameCommandProvider homegameCommandProvider,
            IBunchListPageBuilder bunchListPageBuilder,
            IHomegameDetailsPageBuilder homegameDetailsPageBuilder,
            IAddHomegamePageBuilder addHomegamePageBuilder,
            IAddHomegameConfirmationPageBuilder addHomegameConfirmationPageBuilder,
            IEditHomegamePageBuilder editHomegamePageBuilder,
            IJoinHomegamePageBuilder joinHomegamePageBuilder,
            IJoinHomegameConfirmationPageBuilder joinHomegameConfirmationPageBuilder)
	    {
	        _homegameCommandProvider = homegameCommandProvider;
	        _bunchListPageBuilder = bunchListPageBuilder;
	        _homegameDetailsPageBuilder = homegameDetailsPageBuilder;
	        _addHomegamePageBuilder = addHomegamePageBuilder;
	        _addHomegameConfirmationPageBuilder = addHomegameConfirmationPageBuilder;
	        _editHomegamePageBuilder = editHomegamePageBuilder;
	        _joinHomegamePageBuilder = joinHomegamePageBuilder;
	        _joinHomegameConfirmationPageBuilder = joinHomegameConfirmationPageBuilder;
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
            var model = _addHomegamePageBuilder.Build();
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
            var model = _addHomegamePageBuilder.Build(postModel);
            return View("AddHomegame", model);
		}

        public ActionResult Created()
        {
            var model = _addHomegameConfirmationPageBuilder.Build();
			return View("AddHomegameConfirmation", model);
		}

        [AuthorizeManager]
        public ActionResult Edit(string slug)
        {
            var model = _editHomegamePageBuilder.Build(slug);
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
            var model = _editHomegamePageBuilder.Build(slug, postModel);
            return View("Edit/Edit", model);
		}

        [Authorize]
        public ActionResult Join(string slug)
        {
            var model = _joinHomegamePageBuilder.Build(slug);
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
            var model = _joinHomegamePageBuilder.Build(slug, postModel);
            return View("Join/Join", model);
		}

        [AuthorizePlayer]
		public ActionResult Joined(string slug)
		{
            var model = _joinHomegameConfirmationPageBuilder.Build(slug);
			return View("Join/Confirmation", model);
		}

    }
}