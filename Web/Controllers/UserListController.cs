using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.UserModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class UserListController : PokerBunchController
    {
        [AuthorizeAdmin]
        [Route("-/user/list")]
        public ActionResult List()
        {
            var contextResult = UseCase.AppContext.Execute();
            var showUserListResult = UseCase.UserList.Execute();
            var model = new UserListPageModel(contextResult, showUserListResult);
            return View("~/Views/Pages/UserList/UserList.cshtml", model);
        }
    }
}