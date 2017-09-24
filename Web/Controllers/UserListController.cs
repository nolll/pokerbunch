using System.Web.Mvc;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.UserModels.List;

namespace Web.Controllers
{
    public class UserListController : BaseController
    {
        [Route(WebRoutes.User.List)]
        public ActionResult List()
        {
            var context = GetAppContext();
            var userListResult = UseCase.UserList.Execute();
            var model = new UserListPageModel(context, userListResult);
            return View(model);
        }
    }
}