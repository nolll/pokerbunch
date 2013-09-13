using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.System;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Action{

    public class ActionModel : PageProperties {

        public List<CheckpointModel> Checkpoints { get; set; }
        public UrlModel CashoutUrl { get; set; }
		public UrlModel ChartDataUrl { get; set; }
		public string Heading { get; set; }

        public ActionModel(User user, Homegame homegame, Cashgame cashgame, Player player, CashgameResult result, Role role, List<int> years = null, Cashgame runningGame = null)
            : base (user, homegame, runningGame)
        {
			var dateString = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, true) : string.Empty;
			Heading = string.Format("Cashgame {0}, {1}", dateString, player.DisplayName);
			Checkpoints = GetCheckpointModels(homegame, cashgame, result, player, role);
			ChartDataUrl = new CashgameActionChartJsonUrlModel(homegame, cashgame, player);
        }

		private List<CheckpointModel> GetCheckpointModels(Homegame homegame, Cashgame cashgame, CashgameResult result, Player player, Role role){
			var models = new List<CheckpointModel>();
			var checkpoints = GetCheckpoints(result);
			foreach(var checkpoint in checkpoints){
				models.Add(new CheckpointModel(homegame, cashgame, player, checkpoint, role));
			}
			return models;
		}

		private IEnumerable<Checkpoint> GetCheckpoints(CashgameResult result)
		{
		    return PlayerIsInGame(result) ? result.Checkpoints : new List<Checkpoint>();
		}

        private bool PlayerIsInGame(CashgameResult result){
			return result != null;
		}

        public override string BrowserTitle
        {
            get
            {
                return "Player Actions";
            }
        }

	}

}