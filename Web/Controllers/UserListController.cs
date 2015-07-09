using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.UserModels.List;

namespace Web.Controllers
{
    public class UserListController : PokerBunchController
    {
        [Route("-/user/list")]
        public ActionResult List()
        {
            RequireAdmin();
            var contextResult = GetAppContext();
            var showUserListResult = UseCase.UserList.Execute();
            var model = new UserListPageModel(contextResult, showUserListResult);
            return View("~/Views/Pages/UserList/UserList.cshtml", model);
        }
    }
}