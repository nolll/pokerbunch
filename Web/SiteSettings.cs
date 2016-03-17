using Web.Common;

namespace Web
{
    public class SiteSettings : CommonSettings
    {
        public static string SiteHost => Get("SiteHost");
        public static string ApiHost => Get("ApiHost");
        public static bool IsInProduction => true;//GetBool("InProduction");
    }
}
