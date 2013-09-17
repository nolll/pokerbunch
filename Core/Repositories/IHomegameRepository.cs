using System.Collections.Generic;
using Core.Classes;

namespace Core.Repositories {

	public interface IHomegameRepository{

		Homegame GetByName(string name);
	    IList<Homegame> GetAll();
        IList<Homegame> GetByUser(User user);
	    Role GetHomegameRole(Homegame homegame, User user);
	    Homegame AddHomegame(Homegame homegame);
	    bool SaveHomegame(Homegame homegame);
	}

}