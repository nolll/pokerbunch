using Core.Classes;

namespace Core.Repositories{

	public interface IUserContext{

		string GetToken();
		User GetUser();
		bool IsLoggedIn();
        Role GetRole(Homegame game);
		bool IsInRole(Homegame game, Role roleToCheck);
		bool IsGuest(Homegame game);
		bool IsPlayer(Homegame game);
		bool IsManager(Homegame game);
		bool IsAdmin();
		void RequireUser();
		void RequireRole(Homegame homegame, Role role);
		void RequirePlayer(Homegame homegame);
		void RequireManager(Homegame homegame);
		void RequireAdmin();

	}

}