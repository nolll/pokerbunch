using System.Collections.Generic;
using Core.Classes;

namespace Core.Repositories {

	public interface IHomegameRepository{

		Homegame GetByName(string name);
	    IList<Homegame> GetAll();
	}

}