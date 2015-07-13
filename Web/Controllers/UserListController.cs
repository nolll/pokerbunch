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
            var context = GetAppContext();
            RequireAdmin(context);
            var showUserListResult = UseCase.UserList.Execute();
            var model = new UserListPageModel(context, showUserListResult);
            return View("~/Views/Pages/UserList/UserList.cshtml", model);
        }
    }
}