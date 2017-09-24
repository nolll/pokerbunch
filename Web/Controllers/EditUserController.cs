using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Routes;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.UserModels.Edit;

namespace Web.Controllers
{
    public class EditUserController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.User.Edit)]
        public ActionResult EditUser(string userName)
        {
            return ShowForm(userName);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.User.Edit)]
        public ActionResult Post(string userName, EditUserPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new EditUser.Request(userName, postModel.DisplayName, postModel.RealName, postModel.Email);
                var result = UseCase.EditUser.Execute(request);
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
            var editUserFormResult = UseCase.EditUserForm.Execute(new EditUserForm.Request(userName));
            var model = new EditUserPageModel(contextResult, editUserFormResult, postModel, errors);
            return View(model);
        }
    }
}