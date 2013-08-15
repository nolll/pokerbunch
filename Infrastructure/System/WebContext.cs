using System.Web;

namespace Infrastructure.System{
    public class WebContext : IWebContext{

		public string GetCookie(string name)
		{
		    var cookie = Request.Cookies.Get(name);
		    return cookie == null ? null : cookie.Value;
		}

	    private static HttpRequest Request
	    {
	        get { return HttpContext.Current.Request; }
	    }
	}

}