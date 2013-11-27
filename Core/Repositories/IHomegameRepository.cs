using System.Collections.Generic;
using Core.Classes;

namespace Core.Repositories {

	public interface IHomegameRepository{

		Homegame GetByName(string name);
	    IList<Homegame> GetList();
        IList<Homegame> GetByUser(User user);
	    Role GetHomegameRole(Homegame homegame, User user);
	    Homegame Add(Homegame homegame);
	    bool Save(Homegame homegame);
	    Homegame GetById(int id);
	}

}