using System.Web.Mvc;
using Web.Models.AdminModels;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class AdminController : ControllerBase
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
