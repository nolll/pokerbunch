using System.Web;
using Application.Services;

namespace Infrastructure.Web
{
    public class WebContext : IWebContext
    {
        public string GetCookie(string name)
		{
		    var cookie = Request.Cookies.Get(name);
		    return cookie == null ? null : cookie.Value;
		}

        public string GetQueryParam(string key)
        {
            return Request.QueryString.Get(key);
        }

        public string Host
        {
            get { return Request.Url.Host; }
        }

        private static HttpRequest Request
	    {
	        get { return HttpContext.Current.Request; }
	    }
	}
}