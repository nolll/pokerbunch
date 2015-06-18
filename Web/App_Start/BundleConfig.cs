using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace Web
{
    public static class BundleConfig
    {
        private static readonly List<string> CssFiles = new List<string>
        {
            "normalize",
            "clearfix",
            "grid",
            "site",
            "font-awesome",
            "cashgame",
            "matrix",
            "cashgame-results-form",
            "running-game",
            "standings",
            "player",
            "nav",
            "form",
            "simple-list",
            "value-list",
            "table-list",
            "table-list--sortable",
            "label",
            "icon",
            "checkpoint-list",
            "achievements",
            "spinner",
            "print"
        };

        public static void RegisterBundles(BundleCollection bundles)
        {
            var lessBundle = new LessBundle(BundleUrl).Include(CssUrls);
            //lessBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessBundle);
            BundleTable.EnableOptimizations = true;
        }

        private static string[] CssUrls
        {
            get { return CssFiles.Select(GetCssUrl).ToArray(); }
        }

        public static string BundleUrl
        {
            get
            {
                var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return string.Format("~/-/css/{0}", version.Replace(".", "-"));
            }
        }

        private static string GetCssUrl(string cssName)
        {
            return string.Format("~/FrontEnd/Css/{0}.css", cssName);
        }
    }
}