using System;
using System.Linq;
using System.Reflection;
using Web.Security.Attributes;

namespace Tests.Web
{
    public static class SecurityTestHelper
    {
        public static bool RequiresPlayer(Delegate method)
        {
            var mi = method.GetMethodInfo();
            const bool includeInherited = false;
            return mi.GetCustomAttributes(typeof(AuthorizePlayerAttribute), includeInherited).Any();
        }

        public static bool RequiresManager(Delegate method)
        {
            var mi = method.GetMethodInfo();
            const bool includeInherited = false;
            return mi.GetCustomAttributes(typeof(AuthorizeManagerAttribute), includeInherited).Any();
        }

        public static bool RequiresAdmin(Delegate method)
        {
            var mi = method.GetMethodInfo();
            const bool includeInherited = false;
            return mi.GetCustomAttributes(typeof(AuthorizeAdminAttribute), includeInherited).Any();
        }
    }
}
