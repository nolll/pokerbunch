using System.Web.Mvc;
using Application.UseCases.TestEmail;
using Web.Models.AdminModels;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITestEmailInteractor _testEmailInteractor;

        public AdminController(ITestEmailInteractor testEmailInteractor)
        {
            _testEmailInteractor = testEmailInteractor;
        }

        [AuthorizeAdmin]
        [Route("-/admin/sendemail")]
        public ActionResult SendEmail()
        {
            var result = _testEmailInteractor.Execute();

            var model = new EmailModel(result);

            return View("Email", model);
        }
    }
}
