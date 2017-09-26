using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Join;

namespace Web.Controllers
{
    public class JoinBunchController : BaseController
    {
        [Authorize]
        [Route(JoinBunchUrl.Route)]
        public ActionResult Join(string slug)
        {
            return ShowForm(slug, "");
        }

        [Authorize]
        [Route(JoinBunchUrl.RouteWithCode)]
        public ActionResult Join(string slug, string code)
        {
            return JoinBunch(slug, code);
        }

        [HttpPost]
        [Authorize]
        [Route(JoinBunchUrl.Route)]
        public ActionResult Post(string slug, JoinBunchPostModel postModel)
        {
            var code = postModel != null ? postModel.Code : "";
            return JoinBunch(slug, code);
        }

        private ActionResult JoinBunch(string slug, string code)
        {
            var errors = new List<string>();

            try
            {
                var request = new JoinBunch.Request(slug, code);
                var result = UseCase.JoinBunch.Execute(request);
                return Redirect(new JoinBunchConfirmationUrl(result.BunchId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }
            catch (InvalidJoinCodeException ex)
            {
                errors.Add(ex.Message);
            }

            return ShowForm(slug, code, errors);
        }

        [Authorize]
        [Route(JoinBunchConfirmationUrl.Route)]
        public ActionResult Joined(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var joinBunchConfirmationResult = UseCase.JoinBunchConfirmation.Execute(new JoinBunchConfirmation.Request(slug));
            var model = new JoinBunchConfirmationPageModel(contextResult, joinBunchConfirmationResult);
            return View(model);
        }

        private ActionResult ShowForm(string slug, string code, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var joinBunchFormResult = UseCase.JoinBunchForm.Execute(new JoinBunchForm.Request(slug));
            var model = new JoinBunchPageModel(contextResult, joinBunchFormResult, code, errors);
            return View(model);
        }
    }
}