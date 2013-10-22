using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.Models.ChartModels;

namespace Web.Models.CashgameModels.Chart {

	public class CashgameSuiteChartModel : ChartModel{

		private readonly CashgameSuite _suite;
		private readonly IList<CashgameTotalResult> _results;
		private Dictionary<int, int?> _playerSum;

		public CashgameSuiteChartModel(CashgameSuite suite){
			_suite = suite;
			_results = suite.TotalResults;
			AddChartColumns();
			AddGameRows();
		}

		private void AddGameRows(){
			InitPlayerSumArray();
			AddFirstRow();
			var cashgames = _suite.Cashgames;
			for (var i = 0; i < cashgames.Count; i++){
				var cashgame = cashgames[cashgames.Count - i - 1];
				var currentSum = new Dictionary<int, int?>();
				for (var j = 0; j < _results.Count; j++) {
					var totalResult = _results[j];
					var singleResult = cashgame.GetResult(totalResult.Player);
					var playerId = totalResult.Player.Id;
					if(singleResult != null || i == cashgames.Count - 1){
						var res = singleResult != null ? singleResult.Stack - singleResult.Buyin : 0;
						var sum = _playerSum[playerId] + res;
						_playerSum[playerId] = sum;
						currentSum[playerId] = sum;
					} else {
						currentSum[playerId] = null;
					}
				}
				AddGameRow(cashgame, currentSum);
			}
		}

		private void InitPlayerSumArray(){
			_playerSum = new Dictionary<int, int?>();
			foreach(var result in _results){
				_playerSum[result.Player.Id] = 0;
			}
		}

		private void AddChartColumns(){
			var dateCol = new ChartColumnModel("string", "Date");
			AddColumn(dateCol);

			foreach(var playerResult in _results) {
				var playerCol = new ChartColumnModel("number", playerResult.Player.DisplayName);
				AddColumn(playerCol);
			}
		}

		private void AddFirstRow(){
			var row1 = new ChartRowModel();
			row1.AddValue(new ChartValueModel());
			foreach(var result in _results){
				row1.AddValue(new ChartValueModel(0));
			}
			AddRow(row1);
		}

		private void AddGameRow(Cashgame cashgame, IDictionary<int, int?> currentSum){
			var row1 = new ChartRowModel();
		    var dateStr = cashgame.StartTime.HasValue ? StaticGlobalization.FormatShortDate(cashgame.StartTime.Value) : string.Empty;
            row1.AddValue(new ChartValueModel(dateStr));
			foreach(var result in _results){
				var sum = currentSum[result.Player.Id];
				row1.AddValue(new ChartValueModel(sum));
			}
			AddRow(row1);
		}

	}

}