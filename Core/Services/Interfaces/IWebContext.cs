namespace Core.Services.Interfaces
{
	public interface IWebContext
    {
		string GetCookie(string name);
        string GetQueryParam(string key);
	    string Host { get; }
    }
}