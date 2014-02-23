using System.Collections.Generic;
using Core.Classes;

namespace Application.Factories
{
    public interface ICashgameTotalResultFactory
    {
        IList<CashgameTotalResult> CreateList(IList<Player> players, IList<Cashgame> cashgames);
    }
}