using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Join;

namespace Web.Controllers
{
    public class JoinBunchController : BunchController
    {
        private readonly JoinBunch _joinBunch;
        private readonly JoinBunchConfirmation _joinBunchConfirmation;
        private readonly JoinBunchForm _joinBunchForm;

        public JoinBunchController(
            AppSettings appSettings, 
            CoreContext coreContext, 
            BunchContext bunchContext, 
            JoinBunch joinBunch, 
            JoinBunchConfirmation joinBunchConfirmation, 
            JoinBunchForm joinBunchForm) 
            : base(
                appSettings, 
                coreContext, 
                bunchContext)
        {
            _joinBunch = joinBunch;
            _joinBunchConfirmation = joinBunchConfirmation;
            _joinBunchForm = joinBunchForm;
        }

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
                var result = _joinBunch.Execute(request);
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
            var joinBunchConfirmationResult = _joinBunchConfirmation.Execute(new JoinBunchConfirmation.Request(bunchId));
            var model = new JoinBunchConfirmationPageModel(AppSettings, contextResult, joinBunchConfirmationResult);
            return View(model);
        }

        private ActionResult ShowForm(string bunchId, string code, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var joinBunchFormResult = _joinBunchForm.Execute(new JoinBunchForm.Request(bunchId));
            var model = new JoinBunchPageModel(AppSettings, contextResult, joinBunchFormResult, code, errors);
            return View(model);
        }
    }
}