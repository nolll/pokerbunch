using System.Collections.Generic;
using Core.Classes;

namespace Core.Factories.Interfaces{

    public interface ICashgameTotalResultFactory{

        CashgameTotalResult Create(Player player, IList<CashgameResult> results);
        IList<CashgameTotalResult> CreateList(IEnumerable<Player> players, IDictionary<int, IList<CashgameResult>> resultIndex);

    }

}