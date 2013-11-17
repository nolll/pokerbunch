using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ControllerBase : Controller
    {
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
    }
}