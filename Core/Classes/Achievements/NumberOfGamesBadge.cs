using System.Collections.Generic;
using System.Linq;

namespace Core.Classes.Achievements{

	public class NumberOfGamesBadge : Badge{

	    public bool WasEarned { get; private set; }

		public NumberOfGamesBadge(Player player, IEnumerable<Cashgame> cashgames, int numberToCheck){
			if(cashgames == null){
				WasEarned = false;
				return;
			}
			WasEarned = GetNumberOfPlayedGames(player, cashgames) >= numberToCheck;
		}

		private int GetNumberOfPlayedGames(Player player, IEnumerable<Cashgame> cashgames)
		{
		    return cashgames.Count(cashgame => cashgame.IsInGame(player));
		}
	}

}