using System.Web.Mvc;
using Application.Services;
using Web.Commands.AdminCommands;
using Web.Models.AdminModels;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAuthentication _authentication;
        private readonly IAdminCommandProvider _adminCommandProvider;

        public AdminController(
            IAuthentication authentication,
            IAdminCommandProvider adminCommandProvider)
        {
            _authentication = authentication;
            _adminCommandProvider = adminCommandProvider;
        }

        public ActionResult SendEmail()
        {
            _authentication.RequireAdmin();
            const string to = "henriks@gmail.com";
            var command = _adminCommandProvider.GetEmailTestCommand(to);
            command.Execute();

            var model = new EmailModel(to);

            return View("Email", model);
        }

    }
}
