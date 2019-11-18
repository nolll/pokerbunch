using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Controllers
{
    public class ForgotPasswordController : CoreController
    {
        private readonly ForgotPassword _forgotPassword;

        public ForgotPasswordController(AppSettings appSettings, CoreContext coreContext, ForgotPassword forgotPassword) 
            : base(appSettings, coreContext)
        {
            _forgotPassword = forgotPassword;
        }

        [Route(ForgotPasswordUrl.Route)]
        public ActionResult ForgotPassword()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(ForgotPasswordUrl.Route)]
        public ActionResult Post(ForgotPasswordPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new ForgotPassword.Request(postModel.Email);
                _forgotPassword.Execute(request);
                return Redirect(new ForgotPasswordConfirmationUrl().Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }
            catch (UserNotFoundException ex)
            {
                errors.Add(ex.Message);
            }

            return ShowForm(postModel, errors);
        }

        [Route(ForgotPasswordConfirmationUrl.Route)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new ForgotPasswordConfirmationPageModel(AppSettings, contextResult);
            return View(model);
        }

        private ActionResult ShowForm(ForgotPasswordPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var model = new ForgotPasswordPageModel(AppSettings, contextResult, postModel, errors);
            return View(model);
        }
    }
}