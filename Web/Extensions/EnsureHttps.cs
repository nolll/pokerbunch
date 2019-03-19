using System;
using System.Web.Mvc;
using Environment = Web.Settings.Environment;

namespace Web.Extensions
{
    public class EnsureHttpsAttribute : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            if (filterContext.HttpContext != null && !Environment.IsProd)
                return;

            base.OnAuthorization(filterContext);
        }
    }
}