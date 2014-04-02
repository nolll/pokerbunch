using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
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

        public static bool RequiresOwnUser(Delegate method)
        {
            var mi = method.GetMethodInfo();
            const bool includeInherited = false;
            return mi.GetCustomAttributes(typeof(AuthorizeOwnUserAttribute), includeInherited).Any();
        }

        public static bool RequiresPlayer(Delegate method)
        {
            var mi = method.GetMethodInfo();
            const bool includeInherited = false;
            return mi.GetCustomAttributes(typeof(AuthorizePlayerAttribute), includeInherited).Any();
        }

        public static bool RequiresOwnPlayer(Delegate method)
        {
            var mi = method.GetMethodInfo();
            const bool includeInherited = false;
            return mi.GetCustomAttributes(typeof(AuthorizeOwnPlayerAttribute), includeInherited).Any();
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
