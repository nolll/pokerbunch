using Core.Classes;

namespace Core.Services{

	public interface IAuthentication
    {
        string GetToken();
		User GetUser();
		bool IsLoggedIn();
		bool IsAdmin();
		void RequireUser();
		void RequireAdmin();
	}

}