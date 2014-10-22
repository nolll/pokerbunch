using Core.Entities;
using Infrastructure.SqlServer.Interfaces;
using Infrastructure.Storage;

namespace Infrastructure.SqlServer.Factories.Interfaces
{
    public interface IRawCashgameFactory
    {
        RawCashgame Create(IStorageDataReader reader);
        RawCashgame Create(Cashgame cashgame, GameStatus? status = null);
    }
}