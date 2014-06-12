using System.Web.Mvc;
using Web.Commands.AdminCommands;
using Web.Models.AdminModels;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminCommandProvider _adminCommandProvider;

        public AdminController(
            IAdminCommandProvider adminCommandProvider)
        {
            _adminCommandProvider = adminCommandProvider;
        }

        [AuthorizeAdmin]
        public ActionResult SendEmail()
        {
            const string to = "henriks@gmail.com";
            var command = _adminCommandProvider.GetEmailTestCommand(to);
            command.Execute();

            var model = new EmailModel(to);

            return View("Email", model);
        }

    }
}
