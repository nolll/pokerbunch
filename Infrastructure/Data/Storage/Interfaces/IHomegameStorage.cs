using System.Collections.Generic;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	internal interface IHomegameStorage{

		IList<RawHomegame> GetHomegames();
        IList<RawHomegame> GetHomegamesByUserId(int userId);
        int GetHomegameRole(int homegameId, int userId);
        RawHomegame GetHomegameByName(string name);
        RawHomegame AddHomegame(RawHomegame homegame);
        bool UpdateHomegame(RawHomegame homegame);
		bool DeleteHomegame(string slug);
	    IList<string> GetAllSlugs();
	    IList<RawHomegame> GetHomegames(IList<string> slugs);
	}

}