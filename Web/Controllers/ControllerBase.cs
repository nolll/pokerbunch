using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Plumbing;

namespace Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected DependencyContainer UseCase
        {
            get { return DependencyContainer.Instance; }
        }

        protected void AddModelErrors(IEnumerable<string> errors)
        {
            var i = 0;
            foreach (var error in errors)
            {
                var errorKey = string.Format("error{0}", i);
                ModelState.AddModelError(errorKey, error);
                i++;
            }
        }
        
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult(data, contentType, contentEncoding, behavior);
        }
    }
}