using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Factories{

    public interface ICashgameTotalResultFactory{

        CashgameTotalResult Create(Player player, IList<CashgameResult> results);

	}

}