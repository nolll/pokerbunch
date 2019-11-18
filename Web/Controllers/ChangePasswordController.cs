using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.UserModels.ChangePassword;

namespace Web.Controllers
{
    public class ChangePasswordController : CoreController
    {
        private readonly ChangePassword _changePassword;

        public ChangePasswordController(AppSettings appSettings, CoreContext coreContext, ChangePassword changePassword) 
            : base(appSettings, coreContext)
        {
            _changePassword = changePassword;
        }

        [Authorize]
        [Route(ChangePasswordUrl.Route)]
        public ActionResult ChangePassword()
        {
            return ShowForm();
        }

        [HttpPost]
        [Authorize]
        [Route(ChangePasswordUrl.Route)]
        public ActionResult Post(ChangePasswordPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new ChangePassword.Request(Identity.UserName, postModel.Password, postModel.Repeat);
                _changePassword.Execute(request);
                return Redirect(new ChangePasswordConfirmationUrl().Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(errors);
        }

        private ActionResult ShowForm(IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var model = new ChangePasswordPageModel(AppSettings, contextResult, errors);
            return View(model);
        }

        [Route(ChangePasswordConfirmationUrl.Route)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new ChangePasswordConfirmationPageModel(AppSettings, contextResult);
            return View(model);
        }
    }
}