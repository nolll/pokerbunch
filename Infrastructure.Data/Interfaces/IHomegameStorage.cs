using System.Collections.Generic;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Interfaces {

	public interface IHomegameStorage{

        IList<RawHomegame> GetHomegamesByUserId(int userId);
        int GetHomegameRole(int homegameId, int userId);
        RawHomegame GetHomegameByName(string name);
        RawHomegame AddHomegame(RawHomegame homegame);
        bool UpdateHomegame(RawHomegame homegame);
		bool DeleteHomegame(string slug);
        IList<int> GetAllIds();
	    IList<RawHomegame> GetHomegames(IList<int> ids);
	    int? GetIdBySlug(string slug);
	    RawHomegame GetById(int id);
	}

}