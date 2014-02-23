using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Factories.Interfaces{

	public interface IPlayerDataMapper{

	    Player Create(RawPlayer rawPlayer);

	}

}