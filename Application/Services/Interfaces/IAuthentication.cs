using Core.Classes;

namespace Application.Services{

	public interface IAuthentication
    {
		User GetUser();
		bool IsAdmin();
		void RequireUser();
	}

}