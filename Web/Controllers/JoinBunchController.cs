using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.JoinBunch;
using Core.UseCases.JoinBunchConfirmation;
using Core.UseCases.JoinBunchForm;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Join;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class JoinBunchController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/homegame/join")]
        public ActionResult Join(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/homegame/join")]
        public ActionResult Post(string slug, JoinBunchPostModel postModel)
        {
            try
            {
                var request = new JoinBunchRequest(slug, postModel.Code);
                var result = UseCase.JoinBunch(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            catch (InvalidJoinCodeException ex)
            {
                AddModelError(ex.Message);
            }

            return ShowForm(slug, postModel);
        }

        [AuthorizePlayer]
        [Route("{slug}/homegame/joined")]
        public ActionResult Joined(string slug)
        {
            var contextResult = UseCase.AppContext();
            var joinBunchConfirmationResult = UseCase.JoinBunchConfirmation(new JoinBunchConfirmationRequest(slug));
            var model = new JoinBunchConfirmationPageModel(contextResult, joinBunchConfirmationResult);
            return View("~/Views/Pages/JoinBunch/Confirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, JoinBunchPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext();
            var joinBunchFormResult = UseCase.JoinBunchForm(new JoinBunchFormRequest(slug));
            var model = new JoinBunchPageModel(contextResult, joinBunchFormResult, postModel);
            return View("~/Views/Pages/JoinBunch/Join.cshtml", model);
        }
    }
}