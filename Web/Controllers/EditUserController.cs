using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.UserModels.Edit;
using Web.Urls;

namespace Web.Controllers
{
    public class EditUserController : BaseController
    {
        [Authorize]
        [Route(Routes.UserEdit)]
        public ActionResult EditUser(string userName)
        {
            return ShowForm(userName);
        }

        [HttpPost]
        [Authorize]
        [Route(Routes.UserEdit)]
        public ActionResult Post(string userName, EditUserPostModel postModel)
        {
            try
            {
                var request = new EditUser.Request(userName, postModel.DisplayName, postModel.RealName, postModel.Email);
                var result = UseCase.EditUser.Execute(request);
                return Redirect(new UserDetailsUrl(result.UserName).Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(userName, postModel);
        }

        private ActionResult ShowForm(string userName, EditUserPostModel postModel = null)
        {
            var contextResult = GetAppContext();
            var editUserFormResult = UseCase.EditUserForm.Execute(new EditUserForm.Request(userName));
            var model = new EditUserPageModel(contextResult, editUserFormResult, postModel);
            return View("~/Views/Pages/EditUser/EditUser.cshtml", model);
        }
    }
}