using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Running{

	public class RunningCashgamePageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }

        public string StartTime { get; set; }
        public bool ShowStartTime { get; set; }

        public string Location { get; set; }

        public bool EnableEnd { get; set; }
        public bool BuyinButtonEnabled { get; set; }
        public bool ReportButtonEnabled { get; set; }
        public bool CashoutButtonEnabled { get; set; }
        public bool EndGameButtonEnabled { get; set; }

        public UrlModel BuyinUrl { get; set; }
        public UrlModel ReportUrl { get; set; }
        public UrlModel CashoutUrl { get; set; }
        public UrlModel EndGameUrl { get; set; }

        public RunningCashgameTableModel RunningCashgameTableModel { get; set; }
        public bool ShowTable { get; set; }

        public UrlModel ChartDataUrl { get; set; }
        public bool ShowChart { get; set; }

		public RunningCashgamePageModel(User user, Homegame homegame, Cashgame cashgame, Player player, List<int> years, bool isManager, ITimeProvider timer, Cashgame runningGame = null)
		{
            BrowserTitle = "Running Cashgame";
            PageProperties = new PageProperties(user, homegame, runningGame);
			Location = cashgame.Location;

			if(cashgame.IsStarted){
				ShowStartTime = true;
				StartTime = Globalization.FormatTime(cashgame.StartTime.Value);
			} else {
				ShowStartTime = false;
			}

			SetUrls(homegame, player);
			SetButtons(cashgame, player);

			if(cashgame.IsStarted){
				RunningCashgameTableModel = new RunningCashgameTableModel(homegame, cashgame, isManager, timer);
				ShowTable = true;
			} else {
				ShowTable = false;
			}

			if(cashgame.IsStarted){
				ChartDataUrl = new CashgameDetailsChartJsonUrlModel(homegame, cashgame);
				ShowChart = true;
			} else {
				ShowChart = false;
			}
		}

		public void SetButtons(Cashgame cashgame, Player player){
			var canBeEnded = CanBeEnded(cashgame);
			var canReport = !canBeEnded;
			var isInGame = cashgame.IsInGame(player);

			BuyinButtonEnabled = canReport;
			ReportButtonEnabled = canReport && isInGame;
			CashoutButtonEnabled = isInGame;
			EndGameButtonEnabled = canBeEnded;
		}

		private bool CanBeEnded(Cashgame cashgame){
			return cashgame.IsStarted && !cashgame.HasActivePlayers;
		}

		private void SetUrls(Homegame homegame, Player player){
			BuyinUrl = new CashgameBuyinUrlModel(homegame, player);
			ReportUrl = new CashgameReportUrlModel(homegame, player);
			CashoutUrl = new CashgameCashoutUrlModel(homegame, player);
			EndGameUrl = new CashgameEndUrlModel(homegame);
		}

	}

}