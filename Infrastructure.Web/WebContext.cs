using System.Web;
using Core.Services.Interfaces;

namespace Infrastructure.Web
{
    public class WebContext : IWebContext
    {
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