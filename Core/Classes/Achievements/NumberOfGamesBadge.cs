using System.Collections.Generic;
using System.Linq;

namespace Core.Classes.Achievements{

	public class NumberOfGamesBadge : Badge{

	    public bool WasEarned { get; private set; }

		public NumberOfGamesBadge(int playerId, IEnumerable<Cashgame> cashgames, int numberToCheck){
			if(cashgames == null){
				WasEarned = false;
				return;
			}
			WasEarned = GetNumberOfPlayedGames(playerId, cashgames) >= numberToCheck;
		}

		private int GetNumberOfPlayedGames(int playerId, IEnumerable<Cashgame> cashgames)
		{
		    return cashgames.Count(cashgame => cashgame.IsInGame(playerId));
		}
	}

}