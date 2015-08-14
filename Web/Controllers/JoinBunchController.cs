using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Join;

namespace Web.Controllers
{
    public class JoinBunchController : BaseController
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
                var request = new JoinBunch.Request(slug, CurrentUserName, postModel.Code);
                var result = UseCase.JoinBunch.Execute(request);
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

        [Authorize]
        [Route("{slug}/homegame/joined")]
        public ActionResult Joined(string slug)
        {
            var contextResult = GetBunchContext(slug);
            RequirePlayer(contextResult);
            var joinBunchConfirmationResult = UseCase.JoinBunchConfirmation.Execute(new JoinBunchConfirmation.Request(slug));
            var model = new JoinBunchConfirmationPageModel(contextResult, joinBunchConfirmationResult);
            return View("~/Views/Pages/JoinBunch/Confirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, JoinBunchPostModel postModel = null)
        {
            var contextResult = GetAppContext();
            var joinBunchFormResult = UseCase.JoinBunchForm.Execute(new JoinBunchForm.Request(slug));
            var model = new JoinBunchPageModel(contextResult, joinBunchFormResult, postModel);
            return View("~/Views/Pages/JoinBunch/Join.cshtml", model);
        }
    }
}