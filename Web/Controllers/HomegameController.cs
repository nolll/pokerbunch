using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.AddBunchForm;
using Application.UseCases.AppContext;
using Application.UseCases.BunchList;
using Application.UseCases.JoinBunchForm;
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
	    private readonly IJoinBunchFormInteractor _joinBunchFormInteractor;
	    private readonly IHomegameCommandProvider _homegameCommandProvider;
	    private readonly IHomegameDetailsPageBuilder _homegameDetailsPageBuilder;
	    private readonly IEditHomegamePageBuilder _editHomegamePageBuilder;
	    private readonly IJoinBunchConfirmationPageBuilder _joinBunchConfirmationPageBuilder;

	    public HomegameController(
            IAppContextInteractor appContextInteractor,
            IBunchListInteractor bunchListInteractor,
            IAddBunchFormInteractor addBunchFormInteractor,
            IJoinBunchFormInteractor joinBunchFormInteractor,
            IHomegameCommandProvider homegameCommandProvider,
            IHomegameDetailsPageBuilder homegameDetailsPageBuilder,
            IEditHomegamePageBuilder editHomegamePageBuilder,
            IJoinBunchConfirmationPageBuilder joinBunchConfirmationPageBuilder)
	    {
	        _appContextInteractor = appContextInteractor;
	        _bunchListInteractor = bunchListInteractor;
	        _addBunchFormInteractor = addBunchFormInteractor;
	        _joinBunchFormInteractor = joinBunchFormInteractor;
	        _homegameCommandProvider = homegameCommandProvider;
	        _homegameDetailsPageBuilder = homegameDetailsPageBuilder;
	        _editHomegamePageBuilder = editHomegamePageBuilder;
	        _joinBunchConfirmationPageBuilder = joinBunchConfirmationPageBuilder;
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
        public ActionResult Add(AddBunchPostModel postModel)
        {
            var command = _homegameCommandProvider.GetAddCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new AddBunchConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildAddBunchModel(postModel);
            return View("AddHomegame", model);
		}

	    private AddBunchPageModel BuildAddBunchModel(AddBunchPostModel postModel = null)
	    {
	        var contextResult = _appContextInteractor.Execute();
	        var bunchFormResult = _addBunchFormInteractor.Execute();
	        return new AddBunchPageModel(contextResult, bunchFormResult, postModel);
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
                return Redirect(new BunchDetailsUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = _editHomegamePageBuilder.Build(slug, postModel);
            return View("Edit/Edit", model);
		}

        [Authorize]
        public ActionResult Join(string slug)
        {
            var model = BuildJoinBunchFormModel(slug);
			return View("Join/Join", model);
		}

        [HttpPost]
        [Authorize]
        public ActionResult Join(string slug, JoinHomegamePostModel postModel)
        {
            var command = _homegameCommandProvider.GetJoinCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(new JoinBunchConfirmationUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildJoinBunchFormModel(slug, postModel);
            return View("Join/Join", model);
		}

	    private object BuildJoinBunchFormModel(string slug, JoinHomegamePostModel postModel = null)
	    {
            var contextResult = _appContextInteractor.Execute();

            var joinBunchFormRequest = new JoinBunchFormRequest(slug);
            var joinBunchFormResult = _joinBunchFormInteractor.Execute(joinBunchFormRequest);

            return new JoinHomegamePageModel(contextResult, joinBunchFormResult, postModel);
	    }

        [AuthorizePlayer]
		public ActionResult Joined(string slug)
		{
            var model = _joinBunchConfirmationPageBuilder.Build(slug);
			return View("Join/Confirmation", model);
		}

    }
}