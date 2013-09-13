using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;
using System.Linq;

namespace Web.Models.CashgameModels.Details{

	public class CashgameDetailsPageModel : PageProperties {

	    public string Heading { get; set; }
	    public string Date { get; set; }
	    public string Duration { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string Location { get; set; }

        public bool ShowStartTime { get; set; }
        public bool ShowEndTime { get; set; }

        public bool EnableEdit { get; set; }
        public bool EnableCheckpointsButton { get; set; }
        public bool EnablePublish { get; set; }
        public bool EnableUnpublish { get; set; }
        public bool EnableStart { get; set; }
        public bool EnableEnd { get; set; }
        public bool DurationEnabled { get; set; }

        public UrlModel EditUrl { get; set; }
        public UrlModel CheckpointsUrl { get; set; }
        public UrlModel PublishUrl { get; set; }
        public UrlModel UnpublishUrl { get; set; }
        public UrlModel StartUrl { get; set; }
        public UrlModel EndUrl { get; set; }
        public UrlModel ChartDataUrl { get; set; }

        public string Status { get; set; }

        public CashgameDetailsTableModel CashgameDetailsTableModel { get; set; }

		public CashgameDetailsPageModel(User user, Homegame homegame, Cashgame cashgame, Player player, List<int> years, bool isManager, Cashgame runningGame = null)
            : base(user, homegame, runningGame)
        {
            var dateStr = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;
			Heading = string.Format("Cashgame {0}", dateStr);
			Location = cashgame.Location;

			Duration = Globalization.FormatDuration(cashgame.Duration);
			DurationEnabled = cashgame.Duration > 0;

			ShowStartTime = cashgame.Status >= GameStatus.Running && cashgame.StartTime.HasValue;
			if(ShowStartTime){
				StartTime = Globalization.FormatTime(cashgame.StartTime.Value);
			}

			ShowEndTime = cashgame.Status >= GameStatus.Finished && cashgame.EndTime != null;
			if(ShowEndTime){
				EndTime = Globalization.FormatTime(cashgame.EndTime.Value);
			}

			Status = GameStatusName.GetName(cashgame.Status);

			//var results = GetResults(cashgame);

			SetUrls(homegame, cashgame, player);
			SetButtons(cashgame, player, isManager);

			CashgameDetailsTableModel = new CashgameDetailsTableModel(homegame, cashgame);
		}

        public override string BrowserTitle
        {
            get
            {
                return "Cashgame";
            }
        }

		private void SetButtons(Cashgame cashgame, Player player, bool isManager){
			var numResults = cashgame.Results.Count;
			EnablePublish = PublishButtonVisible(isManager, cashgame.Status, numResults);
			EnableUnpublish = UnpublishButtonVisible(isManager, cashgame.Status);
			EnableEdit = isManager;
			EnableCheckpointsButton = cashgame.IsInGame(player);
		}

		private void SetUrls(Homegame homegame, Cashgame cashgame, Player player){
			PublishUrl = new CashgamePublishUrlModel(homegame, cashgame);
			UnpublishUrl = new CashgameUnpublishUrlModel(homegame, cashgame);
			EditUrl = new CashgameEditUrlModel(homegame, cashgame);
			CheckpointsUrl = new CashgameActionUrlModel(homegame, cashgame, player);
            ChartDataUrl = new CashgameDetailsChartJsonUrlModel(homegame, cashgame);
		}

		private IEnumerable<CashgameResult> GetResults(Cashgame cashgame)
		{
		    return cashgame.Results.OrderBy(o => o.Player.DisplayName);
		}

		private bool PublishButtonVisible(bool isManager, GameStatus status, int numResults){
			return isManager && status == GameStatus.Finished && numResults >= 2;
		}

		private bool UnpublishButtonVisible(bool isManager, GameStatus status){
			return isManager && status == GameStatus.Published;
		}

	}

}