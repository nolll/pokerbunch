using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public interface ICashgameDataMapper
    {
        Cashgame Map(RawCashgame rawGame, IEnumerable<RawCheckpoint> checkpoints);
        IList<Cashgame> MapList(IEnumerable<RawCashgame> rawGames, IEnumerable<RawCheckpoint> checkpoints);
    }
}