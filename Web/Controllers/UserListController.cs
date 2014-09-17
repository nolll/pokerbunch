using System.Web.Mvc;
using Web.Models.UserModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class UserListController : ControllerBase
    {
        [AuthorizeAdmin]
        [Route("-/user/list")]
        public ActionResult List()
        {
            var contextResult = UseCase.AppContext();
            var showUserListResult = UseCase.UserList();
            var model = new UserListPageModel(contextResult, showUserListResult);
            return View("~/Views/Pages/UserList/UserList.cshtml", model);
        }
    }
}