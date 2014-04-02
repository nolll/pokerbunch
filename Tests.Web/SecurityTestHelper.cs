using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Core.Classes;
using Web.Security;

namespace Tests.Web
{
    public static class SecurityTestHelper
    {
        public static bool RequiresUser(Delegate method)
        {
            var mi = method.GetMethodInfo();
            const bool includeInherited = false;
            return mi.GetCustomAttributes(typeof(AuthorizeAttribute), includeInherited).Any();
        }

        public static bool RequiresRole(Delegate method, Role role)
        {
            var mi = method.GetMethodInfo();
            const bool includeInherited = false;
            return mi.GetCustomAttributes(typeof(AuthorizeRoleAttribute), includeInherited).Any(o => ((AuthorizeRoleAttribute)o).Role == role);
        }
    }
}
