using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Factories.Interfaces{

	public interface IPlayerFactory{

	    Player Create(RawPlayer rawPlayer);

	}

}