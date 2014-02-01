using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Factories.Interfaces{

	public interface IUserFactory{

	    User Create(RawUser rawUser);

	}

}