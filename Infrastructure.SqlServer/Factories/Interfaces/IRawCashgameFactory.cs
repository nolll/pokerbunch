using Core.Entities;
using Infrastructure.SqlServer.Classes;
using Infrastructure.SqlServer.Interfaces;

namespace Infrastructure.SqlServer.Factories.Interfaces
{
    public interface IRawCashgameFactory
    {
        RawCashgame Create(IStorageDataReader reader);
        RawCashgame Create(Cashgame cashgame, GameStatus? status = null);
    }
}