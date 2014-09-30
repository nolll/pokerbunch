using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Plumbing;

namespace Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected UseCaseContainer UseCase
        {
            get { return UseCaseContainer.Instance; }
        }

        protected void AddModelErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                AddModelError(error);
            }
        }

        protected void AddModelError(string error)
        {
            ModelState.AddModelError(error, error);
        }
        
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult(data, contentType, contentEncoding, behavior);
        }
    }
}