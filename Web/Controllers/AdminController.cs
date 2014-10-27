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

        [AuthorizeAdmin]
        [Route("-/admin/clearcache")]
        public ActionResult ClearCache()
        {
            var result = UseCase.ClearCache();

            var model = new ClearCacheModel(result);

            return View("ClearCache", model);
        }

        //[AuthorizeAdmin]
        //[Route("-/admin/copytoraven")]
        //public ActionResult CopyToRaven()
        //{
        //    var output = UseCase.CopyToRaven();

        //    var model = new CopyToRavenModel(output);

        //    return View("CopyToRaven", model);
        //}
    }
}
