using System.Web.Mvc;

namespace Web
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            //ViewLocationFormats = new[]
            //{
            //    "~/Views/{1}/{0}.cshtml", "~/Views/Pages/{1}/{0}.cshtml"
            //};

            //PartialViewLocationFormats = ViewLocationFormats;

            //FileExtensions = new[]
            //{
            //    "cshtml"
            //};
        }
    }
}