using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.UserModels.List;
using Web.Urls;

namespace Web.Controllers
{
    public class UserListController : BaseController
    {
        [Route(Routes.UserList)]
        public ActionResult List()
        {
            var context = GetAppContext();
            var userListResult = UseCase.UserList.Execute(new UserList.Request(CurrentUserName));
            var model = new UserListPageModel(context, userListResult);
            return View("~/Views/Pages/UserList/UserList.cshtml", model);
        }
    }
}