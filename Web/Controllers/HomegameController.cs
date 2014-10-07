using System.Web.Mvc;
using Core.Urls;
using Core.UseCases.BunchContext;
using Core.UseCases.BunchDetails;
using Core.UseCases.EditBunchForm;
using Core.UseCases.JoinBunchConfirmation;
using Core.UseCases.JoinBunchForm;
using Web.Commands.HomegameCommands;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Details;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Models.HomegameModels.List;
using Web.Security.Attributes;
using ControllerBase = Web.Controllers.Base.ControllerBase;

namespace Web.Controllers
{
	public class HomegameController : ControllerBase
    {
	    private readonly IBunchCommandProvider _bunchCommandProvider;

	    public HomegameController(
            IBunchCommandProvider bunchCommandProvider)
	    {
	        _bunchCommandProvider = bunchCommandProvider;
	    }

        [AuthorizeAdmin]
        [Route("-/homegame/list")]
        public ActionResult List()
        {
            var contextResult = UseCase.AppContext();
            var bunchListResult = UseCase.BunchList();
            var model = new BunchListPageModel(contextResult, bunchListResult);
			return View("HomegameList", model);
		}

        [AuthorizePlayer]
        [Route("{slug}/homegame/details")]
        public ActionResult Details(string slug)
        {
            var bunchContextRequest = new BunchContextRequest(slug);
            var bunchContextResult = UseCase.BunchContext(bunchContextRequest);

            var bunchDetailsRequest = new BunchDetailsRequest(slug);
            var bunchDetailsResult = UseCase.BunchDetails(bunchDetailsRequest);

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
        [Route("-/homegame/add")]
        public ActionResult Add_Post(AddBunchPostModel postModel)
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

	    [Route("-/homegame/created")]
        public ActionResult Created()
        {
            var contextResult = UseCase.AppContext();
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
        [Route("{slug}/homegame/edit")]
        public ActionResult Edit_Post(string slug, EditBunchPostModel postModel)
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

	    [Authorize]
        [Route("{slug}/homegame/join")]
        public ActionResult Join(string slug)
        {
            var model = BuildJoinBunchFormModel(slug);
			return View("Join/Join", model);
		}

	    [HttpPost]
        [Authorize]
        [Route("{slug}/homegame/join")]
        public ActionResult Join_Post(string slug, JoinBunchPostModel postModel)
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

	    [AuthorizePlayer]
        [Route("{slug}/homegame/joined")]
        public ActionResult Joined(string slug)
		{
            var contextResult = UseCase.AppContext();

            var request = new JoinBunchConfirmationRequest(slug);
            var joinBunchConfirmationResult = UseCase.JoinBunchConfirmation(request);

            var model = new JoinBunchConfirmationPageModel(contextResult, joinBunchConfirmationResult);
			return View("Join/Confirmation", model);
		}

	    private AddBunchPageModel BuildAddBunchModel(AddBunchPostModel postModel = null)
	    {
            var contextResult = UseCase.AppContext();
	        var bunchFormResult = UseCase.AddBunchForm();
	        return new AddBunchPageModel(contextResult, bunchFormResult, postModel);
	    }

	    private EditBunchPageModel BuildEditModel(string slug, EditBunchPostModel postModel = null)
	    {
	        var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));

	        var editBunchFormRequest = new EditBunchFormRequest(slug);
	        var editBunchFormResult = UseCase.EditBunchForm(editBunchFormRequest);

	        return new EditBunchPageModel(contextResult, editBunchFormResult, postModel);
	    }

	    private object BuildJoinBunchFormModel(string slug, JoinBunchPostModel postModel = null)
	    {
            var contextResult = UseCase.AppContext();

	        var joinBunchFormRequest = new JoinBunchFormRequest(slug);
	        var joinBunchFormResult = UseCase.JoinBunchForm(joinBunchFormRequest);

	        return new JoinBunchPageModel(contextResult, joinBunchFormResult, postModel);
	    }
    }
}