using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.UserModels.List;
using Web.Routes;

namespace Web.Controllers
{
    public class UserListController : BaseController
    {
        [Route(WebRoutes.User.List)]
        public ActionResult List()
        {
            var context = GetAppContext();
            var userListResult = UseCase.UserList.Execute(new UserList.Request(Identity.UserName));
            var model = new UserListPageModel(context, userListResult);
            return View("~/Views/Pages/UserList/UserList.cshtml", model);
        }
    }
}