using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IHomegameStorage{

		List<Homegame> GetHomegames();
		List<Homegame> GetHomegamesByRole(string token, int role);
		int GetHomegameRole(Homegame homegame, User user);
        Homegame GetHomegameByName(string name);
        RawHomegame GetRawHomegameByName(string name);
		Homegame AddHomegame(Homegame homegame);
		bool UpdateHomegame(Homegame homegame);
		bool DeleteHomegame(string slug);

	}

}