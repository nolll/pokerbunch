using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.System;
using Web.Models.ChartModels;

namespace Web.Models.CashgameModels.Action {

	public class ActionChartData : ChartModel{

		public ActionChartData(Homegame homegame, Cashgame cashgame, CashgameResult result){
			AddActionColumns();
			AddActionRows(homegame, cashgame, result);
		}

        private void AddActionRows(Homegame homegame, Cashgame cashgame, CashgameResult result)
        {
			var checkpoints = GetCheckpoints(result);
			var totalBuyin = 0;
			foreach(var checkpoint in checkpoints){
				if(checkpoint.Type == CheckpointType.Buyin){
					if(totalBuyin > 0){
						var stackBefore = checkpoint.Stack - checkpoint.Amount;
						AddActionRow(checkpoint.Timestamp, stackBefore, totalBuyin);
					}
					totalBuyin += checkpoint.Amount;
				}
				AddActionRow(checkpoint.Timestamp, checkpoint.Stack, totalBuyin);
			}
			if(cashgame.Status == GameStatus.Running){
				var timestamp = DateTimeFactory.Now(homegame.Timezone);
				AddActionRow(timestamp, result.Stack, result.Buyin);
			}
		}

		private IEnumerable<Checkpoint> GetCheckpoints(CashgameResult result){
			if(PlayerIsInGame(result)){
				return result.Checkpoints;
			} else {
				return new List<Checkpoint>();
			}
		}

		private bool PlayerIsInGame(CashgameResult result){
			return result != null;
		}

		private void AddActionColumns(){
			var timeCol = new ChartDateTimeColumnModel("Time", "HH:mm");
			var stackCol = new ChartNumberColumnModel("Stack");
			var buyinCol = new ChartNumberColumnModel("Buyin");
			AddColumn(timeCol);
			AddColumn(stackCol);
			AddColumn(buyinCol);
		}

		private void AddActionRow(DateTime dateTime, int stack, int buyin){
			var row = new ChartRowModel();
			row.AddValue(new ChartDateTimeValueModel(dateTime));
			row.AddValue(new ChartValueModel(stack));
			row.AddValue(new ChartValueModel(buyin));
			AddRow(row);
		}

	}

}