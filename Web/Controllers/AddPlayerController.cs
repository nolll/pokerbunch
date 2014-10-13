using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.AddPlayer;
using Core.UseCases.BunchContext;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Add;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class AddPlayerController : PokerBunchController
    {
        [AuthorizeManager]
        [Route("{slug}/player/add")]
        public ActionResult Add(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [AuthorizeManager]
        [Route("{slug}/player/add")]
        public ActionResult Add_Post(string slug, AddPlayerPostModel postModel)
        {
            var request = new AddPlayerRequest(slug, postModel.Name);

            try
            {
                var result = UseCase.AddPlayer(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            catch (PlayerExistsException ex)
            {
                AddModelError(ex.Message);
            }

            return ShowForm(slug, postModel);
        }

        [Route("{slug}/player/created")]
        public ActionResult Created(string slug)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new AddPlayerConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddPlayer/AddConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, AddPlayerPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new AddPlayerPageModel(contextResult, postModel);
            return View("~/Views/Pages/AddPlayer/Add.cshtml", model);
        }
    }
}