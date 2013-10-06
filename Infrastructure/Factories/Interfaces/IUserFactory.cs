using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories{

	public interface IUserFactory{

	    User Create(RawUser rawUser);

	}

}