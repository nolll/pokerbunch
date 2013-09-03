using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.System;
using Web.Models.ChartModels;

namespace Web.Views.Cashgame.Chart {

	public class GameChartData : ChartModel{

		public GameChartData(Homegame homegame, Core.Classes.Cashgame cashgame){
			AddActionColumns(cashgame);
			AddActionRows(homegame, cashgame);
		}

		private void AddActionRows(Homegame homegame, Core.Classes.Cashgame cashgame){
			var results = cashgame.Results;
			foreach(var result in results){
				var totalBuyin = 0;
				var checkpoints = result.Checkpoints;
				var playerName = result.Player.DisplayName;
				foreach(var checkpoint in checkpoints){
					if(checkpoint.Type == CheckpointType.Buyin){
						totalBuyin += checkpoint.Amount;
					}
					AddActionRow(cashgame, checkpoint.Timestamp, checkpoint.Stack - totalBuyin, playerName);
				}
			}
			if(cashgame.Status == GameStatus.Running){
				AddCurrentStacks(homegame, results);
			}
		}

		private void AddCurrentStacks(Homegame homegame, IEnumerable<CashgameResult> results){
			var timestamp = DateTimeFactory.Now(homegame.Timezone);
			var row = new ChartRowModel();
			row.AddValue(new ChartDateTimeValueModel(timestamp));
			foreach(var result in results){
				var winnings = result.Stack - result.Buyin;
				row.AddValue(new ChartValueModel(winnings));
			}
			AddRow(row);
		}

		private void AddActionColumns(Core.Classes.Cashgame cashgame){
			AddColumn(new ChartDateTimeColumnModel("Time", "HH:mm"));
			var playerNames = cashgame.GetPlayerNames();
			foreach(var playerName in playerNames){
				AddColumn(new ChartNumberColumnModel(playerName));
			}
		}

		private void AddActionRow(Core.Classes.Cashgame cashgame, DateTime dateTime, int winnings, string currentPlayerName){
			var row1 = new ChartRowModel();
			row1.AddValue(new ChartDateTimeValueModel(dateTime));
			var playerNames = cashgame.GetPlayerNames();
			foreach(var playerName in playerNames){
				string val = null;
				if(playerName == currentPlayerName){
					val = winnings.ToString(CultureInfo.InvariantCulture);
				}
				row1.AddValue(new ChartValueModel(val));
			}
			AddRow(row1);
		}

	}

}