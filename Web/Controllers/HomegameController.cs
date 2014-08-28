using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.AddBunchForm;
using Application.UseCases.AppContext;
using Application.UseCases.BunchList;
using Web.Commands.HomegameCommands;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Models.HomegameModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class HomegameController : ControllerBase
    {
	    private readonly IAppContextInteractor _appContextInteractor;
	    private readonly IBunchListInteractor _bunchListInteractor;
	    private readonly IAddBunchFormInteractor _addBunchFormInteractor;
	    private readonly IHomegameCommandProvider _homegameCommandProvider;
	    private readonly IHomegameDetailsPageBuilder _homegameDetailsPageBuilder;
	    private readonly IEditHomegamePageBuilder _editHomegamePageBuilder;
	    private readonly IJoinHomegamePageBuilder _joinHomegamePageBuilder;
	    private readonly IJoinHomegameConfirmationPageBuilder _joinHomegameConfirmationPageBuilder;

	    public HomegameController(
            IAppContextInteractor appContextInteractor,
            IBunchListInteractor bunchListInteractor,
            IAddBunchFormInteractor addBunchFormInteractor,
            IHomegameCommandProvider homegameCommandProvider,
            IHomegameDetailsPageBuilder homegameDetailsPageBuilder,
            IEditHomegamePageBuilder editHomegamePageBuilder,
            IJoinHomegamePageBuilder joinHomegamePageBuilder,
            IJoinHomegameConfirmationPageBuilder joinHomegameConfirmationPageBuilder)
	    {
	        _appContextInteractor = appContextInteractor;
	        _bunchListInteractor = bunchListInteractor;
	        _addBunchFormInteractor = addBunchFormInteractor;
	        _homegameCommandProvider = homegameCommandProvider;
	        _homegameDetailsPageBuilder = homegameDetailsPageBuilder;
	        _editHomegamePageBuilder = editHomegamePageBuilder;
	        _joinHomegamePageBuilder = joinHomegamePageBuilder;
	        _joinHomegameConfirmationPageBuilder = joinHomegameConfirmationPageBuilder;
	    }

        [AuthorizeAdmin]
	    public ActionResult List()
        {
            var contextResult = _appContextInteractor.Execute();
            var bunchListResult = _bunchListInteractor.Execute();
            var model = new BunchListPageModel(contextResult, bunchListResult);
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
            var model = BuildAddBunchModel();
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
            var model = BuildAddBunchModel(postModel);
            return View("AddHomegame", model);
		}

	    private AddHomegamePageModel BuildAddBunchModel(AddHomegamePostModel postModel = null)
	    {
	        var contextResult = _appContextInteractor.Execute();
	        var bunchFormResult = _addBunchFormInteractor.Execute();
	        return new AddHomegamePageModel(contextResult, bunchFormResult, postModel);
	    }

	    public ActionResult Created()
        {
            var contextResult = _appContextInteractor.Execute();
            var model = new AddHomegameConfirmationPageModel(contextResult);
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