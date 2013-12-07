using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var lessBundle = new StyleBundle(CssUrl).Include(
                "~/FrontEnd/Css/normalize.css",
                "~/FrontEnd/Css/clearfix.css",
                "~/FrontEnd/Css/grid.css",
                "~/FrontEnd/Css/site.css",
                "~/FrontEnd/Css/font-awesome.css",
                "~/FrontEnd/Css/cashgame.css",
                "~/FrontEnd/Css/cashgame-toplist.css",
                "~/FrontEnd/Css/cashgame-matrix.css");
            lessBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessBundle);
        }

        public static string CssUrl
        {
            get
            {
                var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return string.Format("~/-/css/{0}", version.Replace(".", "-"));
            }
        }
    }
}