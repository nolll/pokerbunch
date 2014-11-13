using System.Collections.Generic;
using System.Linq;
using Core.UseCases.RunningCashgame;

namespace Web.Models.CashgameModels.Board
{
    public class CashgameBoardTableModel
    {
	    public IList<CashgameBoardTableItemModel> StatusModels { get; private set; }
	    public string TotalBuyin { get; private set; }
	    public string TotalStacks { get; private set; }

        public CashgameBoardTableModel(RunningCashgameResult result)
        {
            StatusModels = result.Items.Select(o => new CashgameBoardTableItemModel(o)).ToList();
            TotalBuyin = result.TotalBuyin.String;
            TotalStacks = result.TotalStacks.String;
        }
    }
}