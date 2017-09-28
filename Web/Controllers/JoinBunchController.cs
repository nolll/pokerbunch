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
        public ActionResult Join(string bunchId)
        {
            return ShowForm(bunchId, "");
        }

        [Authorize]
        [Route(JoinBunchUrl.RouteWithCode)]
        public ActionResult Join(string bunchId, string code)
        {
            return JoinBunch(bunchId, code);
        }

        [HttpPost]
        [Authorize]
        [Route(JoinBunchUrl.Route)]
        public ActionResult Post(string bunchId, JoinBunchPostModel postModel)
        {
            var code = postModel != null ? postModel.Code : "";
            return JoinBunch(bunchId, code);
        }

        private ActionResult JoinBunch(string bunchId, string code)
        {
            var errors = new List<string>();

            try
            {
                var request = new JoinBunch.Request(bunchId, code);
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

            return ShowForm(bunchId, code, errors);
        }

        [Authorize]
        [Route(JoinBunchConfirmationUrl.Route)]
        public ActionResult Joined(string bunchId)
        {
            var contextResult = GetBunchContext(bunchId);
            var joinBunchConfirmationResult = UseCase.JoinBunchConfirmation.Execute(new JoinBunchConfirmation.Request(bunchId));
            var model = new JoinBunchConfirmationPageModel(contextResult, joinBunchConfirmationResult);
            return View(model);
        }

        private ActionResult ShowForm(string bunchId, string code, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var joinBunchFormResult = UseCase.JoinBunchForm.Execute(new JoinBunchForm.Request(bunchId));
            var model = new JoinBunchPageModel(contextResult, joinBunchFormResult, code, errors);
            return View(model);
        }
    }
}