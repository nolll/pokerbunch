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

        public RunningCashgameJsonModel(RunningCashgameResult result)
        {
            Players = result.PlayerItems.Select(o => new RunningCashgamePlayerJsonModel(o)).ToList();
        }
    }
}