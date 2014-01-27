using Core.Classes;

namespace Application.Services.Interfaces{

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