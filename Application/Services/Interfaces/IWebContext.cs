namespace Application.Services
{
	public interface IWebContext
    {
		string GetCookie(string name);
        string GetQueryParam(string key);
	    string Host { get; }
    }
}