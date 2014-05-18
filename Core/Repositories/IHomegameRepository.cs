using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories {

	public interface IHomegameRepository{

		Homegame GetBySlug(string name);
	    IList<Homegame> GetList();
        IList<Homegame> GetByUser(User user);
	    Role GetHomegameRole(Homegame homegame, User user);
	    Homegame Add(Homegame homegame);
	    bool Save(Homegame homegame);
	    Homegame GetById(int id);
	}

}