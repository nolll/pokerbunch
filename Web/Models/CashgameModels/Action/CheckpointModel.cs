using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.System;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Action{
    public class CheckpointModel {

		public string Description { get; set; }
		public string Stack { get; set; }
		public string Timestamp { get; set; }
		public bool ShowLink { get; set; }
		public UrlModel EditUrl { get; set; }

		public CheckpointModel(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint, Role role){
			Description = checkpoint.Description;
			Stack = Globalization.FormatCurrency(homegame.Currency, checkpoint.Stack);
			Timestamp = Globalization.FormatTime(checkpoint.Timestamp);
			ShowLink = role >= Role.Manager;
			EditUrl = new CashgameCheckpointDeleteUrlModel(homegame, cashgame, player, checkpoint);
		}

	}

}