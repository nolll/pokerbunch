using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Achievements;

namespace Web.Models.PlayerModels.Achievements{

	public class PlayerBadgesModel{

	    public bool PlayedOneGame { get; set; }
        public bool PlayedTenGames { get; set; }
        public bool Played50Games { get; set; }
        public bool Played100Games { get; set; }
        public bool Played200Games { get; set; }
        public bool Played500Games { get; set; }
        
        public PlayerBadgesModel(Player player, List<Cashgame> cashgames){
			if(player == null || cashgames == null){
				return;
			}
			SetNumberOfGamesBadges(player, cashgames);
		}

		private void SetNumberOfGamesBadges(Player player, List<Cashgame> cashgames){
			var n1 = new NumberOfGamesBadge(player, cashgames, 1);
            PlayedOneGame = n1.WasEarned;

            var n10 = new NumberOfGamesBadge(player, cashgames, 10);
            PlayedTenGames = n10.WasEarned;

            var n50 = new NumberOfGamesBadge(player, cashgames, 50);
            Played50Games = n50.WasEarned;

            var n100 = new NumberOfGamesBadge(player, cashgames, 100);
            Played100Games = n100.WasEarned;

            var n200 = new NumberOfGamesBadge(player, cashgames, 200);
            Played200Games = n200.WasEarned;

            var n500 = new NumberOfGamesBadge(player, cashgames, 500);
			Played500Games = n500.WasEarned;
		}

	}

}