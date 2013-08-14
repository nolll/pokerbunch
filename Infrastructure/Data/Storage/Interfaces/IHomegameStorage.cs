using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface HomegameStorage{

		List<Homegame> GetHomegames();
		List<Homegame> GetHomegamesByRole(string token, int role);
		int GetHomegameRole(Homegame homegame, User user);
        Homegame GetHomegameByName(string name);
        RawHomegame GetRawHomegameByName(string name);
		Homegame AddHomegame(Homegame homegame);
		bool UpdateHomegame(Homegame homegame);
		bool DeleteHomegame(Homegame homegame);

	}

}