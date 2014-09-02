using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.AddBunchForm;
using Application.UseCases.AppContext;
using Application.UseCases.BunchContext;
using Application.UseCases.BunchDetails;
using Application.UseCases.BunchList;
using Application.UseCases.EditBunchForm;
using Application.UseCases.JoinBunchConfirmation;
using Application.UseCases.JoinBunchForm;
using Web.Commands.HomegameCommands;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Details;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Models.HomegameModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
	public class HomegameController : ControllerBase
    {
	    private readonly IAppContextInteractor _appContextInteractor;
	    private readonly IBunchContextInteractor _bunchContextInteractor;
	    private readonly IBunchListInteractor _bunchListInteractor;
	    private readonly IAddBunchFormInteractor _addBunchFormInteractor;
	    private readonly IJoinBunchFormInteractor _joinBunchFormInteractor;
	    private readonly IJoinBunchConfirmationInteractor _joinBunchConfirmationInteractor;
	    private readonly IBunchDetailsInteractor _bunchDetailsInteractor;
	    private readonly IEditBunchFormInteractor _editBunchFormInteractor;
	    private readonly IBunchCommandProvider _bunchCommandProvider;

	    public HomegameController(
            IAppContextInteractor appContextInteractor,
            IBunchContextInteractor bunchContextInteractor,
            IBunchListInteractor bunchListInteractor,
            IAddBunchFormInteractor addBunchFormInteractor,
            IJoinBunchFormInteractor joinBunchFormInteractor,
            IJoinBunchConfirmationInteractor joinBunchConfirmationInteractor,
            IBunchDetailsInteractor bunchDetailsInteractor,
            IEditBunchFormInteractor editBunchFormInteractor,
            IBunchCommandProvider bunchCommandProvider)
	    {
	        _appContextInteractor = appContextInteractor;
	        _bunchContextInteractor = bunchContextInteractor;
	        _bunchListInteractor = bunchListInteractor;
	        _addBunchFormInteractor = addBunchFormInteractor;
	        _joinBunchFormInteractor = joinBunchFormInteractor;
	        _joinBunchConfirmationInteractor = joinBunchConfirmationInteractor;
	        _bunchDetailsInteractor = bunchDetailsInteractor;
	        _editBunchFormInteractor = editBunchFormInteractor;
	        _bunchCommandProvider = bunchCommandProvider;
	    }

        [AuthorizeAdmin]
        [Route("-/homegame/list")]
        public ActionResult List()
        {
            var contextResult = _appContextInteractor.Execute();
            var bunchListResult = _bunchListInteractor.Execute();
            var model = new BunchListPageModel(contextResult, bunchListResult);
			return View("HomegameList", model);
		}

        [AuthorizePlayer]
        [Route("{slug}/homegame/details")]
        public ActionResult Details(string slug)
        {
            var bunchContextRequest = new BunchContextRequest(slug);
            var bunchContextResult = _bunchContextInteractor.Execute(bunchContextRequest);

            var bunchDetailsRequest = new BunchDetailsRequest(slug);
            var bunchDetailsResult = _bunchDetailsInteractor.Execute(bunchDetailsRequest);

            var model = new BunchDetailsPageModel(bunchContextResult, bunchDetailsResult);
			return View("HomegameDetails", model);
		}

        [Authorize]
        [Route("-/homegame/add")]
        public ActionResult Add()
        {
            var model = BuildAddBunchModel();
            return View("AddHomegame", model);
        }

	    [HttpPost]
        [Authorize]
        //todo: add route
        public ActionResult Add(AddBunchPostModel postModel)
        {
            var command = _bunchCommandProvider.GetAddCommand(postModel);
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

        [Route("-/homegame/created")]
        public ActionResult Created()
        {
            var contextResult = _appContextInteractor.Execute();
            var model = new AddBunchConfirmationPageModel(contextResult);
			return View("AddHomegameConfirmation", model);
		}

        [AuthorizeManager]
        [Route("{slug}/homegame/edit")]
        public ActionResult Edit(string slug)
        {
            var model = BuildEditModel(slug);
			return View("Edit/Edit", model);
		}

        [HttpPost]
        [AuthorizeManager]
        //todo: add route
        public ActionResult Edit(string slug, EditBunchPostModel postModel)
        {
            var command = _bunchCommandProvider.GetEditCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(new BunchDetailsUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildEditModel(slug, postModel);
            return View("Edit/Edit", model);
		}

        private EditBunchPageModel BuildEditModel(string slug, EditBunchPostModel postModel = null)
	    {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));

            var editBunchFormRequest = new EditBunchFormRequest(slug);
            var editBunchFormResult = _editBunchFormInteractor.Execute(editBunchFormRequest);

            return new EditBunchPageModel(contextResult, editBunchFormResult, postModel);
	    }

        [Authorize]
        //todo: add route
        public ActionResult Join(string slug)
        {
            var model = BuildJoinBunchFormModel(slug);
			return View("Join/Join", model);
		}

        [HttpPost]
        [Authorize]
        //todo: add route
        public ActionResult Join(string slug, JoinBunchPostModel postModel)
        {
            var command = _bunchCommandProvider.GetJoinCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(new JoinBunchConfirmationUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildJoinBunchFormModel(slug, postModel);
            return View("Join/Join", model);
		}

	    private object BuildJoinBunchFormModel(string slug, JoinBunchPostModel postModel = null)
	    {
            var contextResult = _appContextInteractor.Execute();

            var joinBunchFormRequest = new JoinBunchFormRequest(slug);
            var joinBunchFormResult = _joinBunchFormInteractor.Execute(joinBunchFormRequest);

            return new JoinBunchPageModel(contextResult, joinBunchFormResult, postModel);
	    }

        [AuthorizePlayer]
        //todo: add route
        public ActionResult Joined(string slug)
		{
            var contextResult = _appContextInteractor.Execute();

            var request = new JoinBunchConfirmationRequest(slug);
            var joinBunchConfirmationResult = _joinBunchConfirmationInteractor.Execute(request);

            var model = new JoinBunchConfirmationPageModel(contextResult, joinBunchConfirmationResult);
			return View("Join/Confirmation", model);
		}

    }
}