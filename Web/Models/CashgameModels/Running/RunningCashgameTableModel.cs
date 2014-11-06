using System.Collections.Generic;
using System.Linq;
using Core.UseCases.RunningCashgame;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameTableModel
    {
	    public List<RunningCashgameTableItemModel> StatusModels { get; private set; }
	    public string TotalBuyin { get; private set; }
	    public string TotalStacks { get; private set; }

        public RunningCashgameTableModel(RunningCashgameResult result)
        {
            StatusModels = result.Items.Select(o => new RunningCashgameTableItemModel(o)).ToList();
            TotalBuyin = result.TotalBuyin.String;
            TotalStacks = result.TotalStacks.String;
        }
    }
}