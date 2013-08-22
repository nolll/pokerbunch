using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IHomegameStorage{

		IList<RawHomegame> GetHomegames();
		IList<Homegame> GetHomegamesByRole(string token, int role);
		int GetHomegameRole(Homegame homegame, User user);
        RawHomegame GetHomegameByName(string name);
		Homegame AddHomegame(Homegame homegame);
		bool UpdateHomegame(Homegame homegame);
		bool DeleteHomegame(string slug);

	}

}