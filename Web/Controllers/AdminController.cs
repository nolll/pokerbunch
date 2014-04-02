using System.Web.Mvc;
using Core.Classes;
using Web.Commands.AdminCommands;
using Web.Models.AdminModels;
using Web.Security;
using Web.Services;

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

        [AuthorizeRole(Role = Role.Admin)]
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
