using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Join;
using Web.Urls;

namespace Web.Controllers
{
    public class JoinBunchController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.BunchJoin)]
        public ActionResult Join(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.BunchJoin)]
        public ActionResult Post(string slug, JoinBunchPostModel postModel)
        {
            try
            {
                var request = new JoinBunch.Request(slug, CurrentUserName, postModel.Code);
                var result = UseCase.JoinBunch.Execute(request);
                return Redirect(new JoinBunchConfirmationUrl(result.Slug).Relative);
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
        [Route(WebRoutes.BunchJoinConfirmation)]
        public ActionResult Joined(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var joinBunchConfirmationResult = UseCase.JoinBunchConfirmation.Execute(new JoinBunchConfirmation.Request(CurrentUserName, slug));
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