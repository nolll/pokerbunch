using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Facts{

	public class CashgameFactsPageModel : HomegamePageModel {

	    public CashgameNavigationModel CashgameNavModel { get; set; }
	    public int GameCount { get; set; }
		public string TotalGameTime { get; set; }
		public string BestResultAmount { get; set; }
		public string BestResultName { get; set; }
		public string WorstResultAmount { get; set; }
		public string WorstResultName { get; set; }
		public string MostTimeDuration { get; set; }
		public string MostTimeName { get; set; }

		public CashgameFactsPageModel(User user, Homegame homegame, CashgameSuite suite, IList<int> years, int? year = null, Cashgame runningGame = null)
            : base(user, homegame, runningGame)
        {
			GameCount = suite.GameCount;
			TotalGameTime = Globalization.FormatDuration(suite.TotalGameTime);
			SetBestResult(homegame, suite.BestResult);
			SetWorstResult(homegame, suite.WorstResult);
			SetMostTime(suite.MostTimeResult);
			CashgameNavModel = new CashgameNavigationModel(homegame, "facts", years, year, runningGame);
		}

		private void SetBestResult(Homegame homegame, CashgameResult result){
			if(result != null){
				BestResultAmount = Globalization.FormatResult(homegame.Currency, result.Winnings);
				var player = result.Player;
				if(player != null){
					BestResultName = player.DisplayName;
				}
			}
		}

		private void SetWorstResult(Homegame homegame, CashgameResult result){
			if(result != null){
				WorstResultAmount = Globalization.FormatResult(homegame.Currency, result.Winnings);
				var player = result.Player;
				if(player != null){
					WorstResultName = player.DisplayName;
				}
			}
		}

		private void SetMostTime(CashgameTotalResult result){
			if(result != null){
				MostTimeDuration = Globalization.FormatDuration(result.TimePlayed);
				var player = result.Player;
				if(player != null){
					MostTimeName = player.DisplayName;
				}
			}
		}

        public override string BrowserTitle
        {
            get { return "Cashgame Facts"; }
        }

	}

}