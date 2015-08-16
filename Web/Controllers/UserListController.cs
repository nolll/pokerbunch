using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.UserModels.List;

namespace Web.Controllers
{
    public class UserListController : BaseController
    {
        [Route(Routes.UserList)]
        public ActionResult List()
        {
            var context = GetAppContext();
            var showUserListResult = UseCase.UserList.Execute(new UserList.Request(CurrentUserName));
            var model = new UserListPageModel(context, showUserListResult);
            return View("~/Views/Pages/UserList/UserList.cshtml", model);
        }
    }
}