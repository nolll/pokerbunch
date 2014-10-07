using System.Web.Mvc;

namespace Web
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/Views/{0}.cshtml", "~/Views/Pages/{0}.cshtml", "~/Views/{1}/{0}.cshtml"
            };

            PartialViewLocationFormats = ViewLocationFormats;

            FileExtensions = new[]
            {
                "cshtml"
            };
        }
    }
}