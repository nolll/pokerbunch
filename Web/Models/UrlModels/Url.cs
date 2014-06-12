using System.Web.Configuration;

namespace Web.Models.UrlModels
{
    public class Url
    {
        public string Relative { get; private set; }

        public Url(string url)
        {
            Relative = url ?? string.Empty;
        }

        public string Absolute
        {
            get
            {
                var siteUrl = WebConfigurationManager.AppSettings.Get("SiteUrl");
                return string.Concat(siteUrl, Relative);
            }
        }

        public override string ToString()
        {
            return Relative;
        }

        public virtual bool IsEmpty()
        {
            return false;
        }
    }
}