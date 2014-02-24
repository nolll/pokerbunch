using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers{

	public interface IPlayerDataMapper{

	    Player Create(RawPlayer rawPlayer);

	}

}