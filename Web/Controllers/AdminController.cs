using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.AdminModels;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class AdminController : PokerBunchController
    {
        [AuthorizeAdmin]
        [Route("-/admin/sendemail")]
        public ActionResult SendEmail()
        {
            var result = UseCase.TestEmail();

            var model = new EmailModel(result);

            return View("Email", model);
        }
    }
}
