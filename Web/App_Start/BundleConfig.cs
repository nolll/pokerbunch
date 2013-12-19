using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var lessBundle = new StyleBundle(BundleUrl).Include(
                GetCssUrl("normalize"),
                GetCssUrl("clearfix"),
                GetCssUrl("grid"),
                GetCssUrl("site"),
                GetCssUrl("font-awesome"),
                GetCssUrl("cashgame"),
                GetCssUrl("sortable-table"),
                GetCssUrl("cashgame-matrix"),
                GetCssUrl("cashgame-results-form"),
                GetCssUrl("running-game"),
                GetCssUrl("standings"),
                GetCssUrl("facts"),
                GetCssUrl("player"),
                GetCssUrl("nav"),
                GetCssUrl("form"),
                GetCssUrl("list"),
                GetCssUrl("icon"),
                GetCssUrl("checkpoint-list"),
                GetCssUrl("print"));
            lessBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessBundle);
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