using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories{

	public interface IPlayerFactory{

	    Player Create(RawPlayer rawPlayer);

	}

}