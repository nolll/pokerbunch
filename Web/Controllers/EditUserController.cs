using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.UserModels.Edit;

namespace Web.Controllers
{
    public class EditUserController : CoreController
    {
        private readonly EditUser _editUser;
        private readonly EditUserForm _editUserForm;

        public EditUserController(AppSettings appSettings, CoreContext coreContext, EditUser editUser, EditUserForm editUserForm)
            : base(appSettings, coreContext)
        {
            _editUser = editUser;
            _editUserForm = editUserForm;
        }

        [Authorize]
        [Route(EditUserUrl.Route)]
        public ActionResult EditUser(string userName)
        {
            return ShowForm(userName);
        }

        [HttpPost]
        [Authorize]
        [Route(EditUserUrl.Route)]
        public ActionResult Post(string userName, EditUserPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new EditUser.Request(userName, postModel.DisplayName, postModel.RealName, postModel.Email);
                var result = _editUser.Execute(request);
                return Redirect(new UserDetailsUrl(result.UserName).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(userName, postModel, errors);
        }

        private ActionResult ShowForm(string userName, EditUserPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var editUserFormResult = _editUserForm.Execute(new EditUserForm.Request(userName));
            var model = new EditUserPageModel(AppSettings, contextResult, editUserFormResult, postModel, errors);
            return View(model);
        }
    }
}