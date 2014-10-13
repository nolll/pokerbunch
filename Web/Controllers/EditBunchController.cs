using System.Web.Mvc;
using Core.Urls;
using Core.UseCases.BunchContext;
using Core.UseCases.EditBunchForm;
using Web.Commands.HomegameCommands;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Edit;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EditBunchController : PokerBunchController
    {
        private readonly IBunchCommandProvider _bunchCommandProvider;

        public EditBunchController(IBunchCommandProvider bunchCommandProvider)
        {
            _bunchCommandProvider = bunchCommandProvider;
        }

        [AuthorizeManager]
        [Route("{slug}/homegame/edit")]
        public ActionResult Edit(string slug)
        {
            var model = BuildEditModel(slug);
            return View("~/Views/Pages/EditBunch/Edit.cshtml", model);
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
            return View("~/Views/Pages/EditBunch/Edit.cshtml", model);
        }

        private EditBunchPageModel BuildEditModel(string slug, EditBunchPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));

            var editBunchFormRequest = new EditBunchFormRequest(slug);
            var editBunchFormResult = UseCase.EditBunchForm(editBunchFormRequest);

            return new EditBunchPageModel(contextResult, editBunchFormResult, postModel);
        }
    }
}