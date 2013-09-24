using Core.Classes;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Running{

	public class BarModel{

		public bool GameIsRunning;
		public UrlModel GameUrl;

		public BarModel(Homegame homegame, Cashgame runningGame){
			if(runningGame != null){
				GameIsRunning = true;
				GameUrl = new RunningCashgameUrlModel(homegame);
			}
		}

	}

}