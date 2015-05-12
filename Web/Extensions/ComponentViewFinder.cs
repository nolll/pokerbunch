using System;

namespace Web.Extensions
{
    public static class ComponentViewFinder
    {
        private const string ViewNameFormat = "~/{0}.cshtml";

        public static string GetViewFor(Type t)
        {
            return GetViewName(t);
        }

        private static string GetViewName(Type t)
        {
            return string.Format(ViewNameFormat, t.FullName.Replace("Web.", "").Replace(".", "/"));
        }
    }
}