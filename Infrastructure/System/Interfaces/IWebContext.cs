namespace Infrastructure.System{

	public interface IWebContext{

		string GetCookie(string name);
        string GetPostParam(string key);
        string GetQueryParam(string key);
	    string GetHost();
	    void ClearCookie(string name);
	    void SetSessionCookie(string name, string value);
	    void SetPersistentCookie(string name, string value);
	}

}