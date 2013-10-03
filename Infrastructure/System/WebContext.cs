using System.Web;

namespace Infrastructure.System{
    public class WebContext : IWebContext{
        private readonly ITimeProvider _timeProvider;

        public WebContext(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public string GetCookie(string name)
		{
		    var cookie = Request.Cookies.Get(name);
		    return cookie == null ? null : cookie.Value;
		}

        public string GetPostParam(string key)
        {
            return Request.Form.Get(key);
        }

        public string GetQueryParam(string key)
        {
            return Request.QueryString.Get(key);
        }

        public string GetHost()
        {
            return Request.Url.Host.Replace("www.", "");
        }

        public void ClearCookie(string token)
        {
            if (Request.Cookies[token] != null)
            {
                var myCookie = new HttpCookie(token) {Expires = _timeProvider.GetTime().AddDays(-1)};
                Response.Cookies.Add(myCookie);
            }
        }

        public void SetSessionCookie(string name, string value)
        {
            var cookie = CreateCookie(name, value);
            Response.SetCookie(cookie);
        }

        public void SetPersistentCookie(string name, string value)
        {
            var cookie = CreateCookie(name, value);
            cookie.Expires = _timeProvider.GetTime().AddYears(3);
            Response.SetCookie(cookie);
        }

        public void SetSession(string name, object value)
        {
            HttpContext.Current.Session[name] = value;
        }

        public object GetSession(string name)
        {
            return HttpContext.Current.Session[name];
        }

        public void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        private HttpCookie CreateCookie(string name, string value)
        {
            return new HttpCookie(name, value);
        }

        private static HttpRequest Request
	    {
	        get { return HttpContext.Current.Request; }
	    }

        private static HttpResponse Response
        {
            get { return HttpContext.Current.Response; }
        }

	}

}