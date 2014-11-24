using System.Collections.Generic;
using System.Linq;
using Core.UseCases.RunningCashgame;
using Web.Annotations;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameJsonModel
    {
        [UsedImplicitly]
	    public List<RunningCashgamePlayerJsonModel> Players { get; private set; }
        
        [UsedImplicitly]
        public int PlayerId { get; private set; }
        
        [UsedImplicitly]
        public string ReportUrl { get; private set; }

        public RunningCashgameJsonModel(RunningCashgameResult result)
        {
            PlayerId = result.PlayerId;
            ReportUrl = result.ReportUrl.Relative;
            Players = result.PlayerItems.Select(o => new RunningCashgamePlayerJsonModel(o)).ToList();
        }
    }
}