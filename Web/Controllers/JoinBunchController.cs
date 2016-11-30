using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Join;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class JoinBunchController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Bunch.Join)]
        public ActionResult Join(string slug)
        {
            return ShowForm(slug, "");
        }

        [Authorize]
        [Route(WebRoutes.Bunch.JoinWithCode)]
        public ActionResult Join(string slug, string code)
        {
            return JoinBunch(slug, code);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Bunch.Join)]
        public ActionResult Post(string slug, JoinBunchPostModel postModel)
        {
            var code = postModel != null ? postModel.Code : "";
            return JoinBunch(slug, code);
        }

        private ActionResult JoinBunch(string slug, string code)
        {
            try
            {
                var request = new JoinBunch.Request(slug, Identity.UserName, code);
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

            return ShowForm(slug, code);
        }

        [Authorize]
        [Route(WebRoutes.Bunch.JoinConfirmation)]
        public ActionResult Joined(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var joinBunchConfirmationResult = UseCase.JoinBunchConfirmation.Execute(new JoinBunchConfirmation.Request(Identity.UserName, slug));
            var model = new JoinBunchConfirmationPageModel(contextResult, joinBunchConfirmationResult);
            return View("~/Views/Pages/JoinBunch/Confirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, string code)
        {
            var contextResult = GetAppContext();
            var joinBunchFormResult = UseCase.JoinBunchForm.Execute(new JoinBunchForm.Request(slug));
            var model = new JoinBunchPageModel(contextResult, joinBunchFormResult, code);
            return View("~/Views/Pages/JoinBunch/Join.cshtml", model);
        }
    }
}