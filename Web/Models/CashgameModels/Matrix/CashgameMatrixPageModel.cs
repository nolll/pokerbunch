using Application.Urls;
using Application.UseCases.CashgameContext;
using Web.Models.CashgameModels.Running;
using Web.Models.NavigationModels;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixPageModel : CashgameContextPageModel
    {
        public BarModel BarModel { get; set; }
        public bool GameIsRunning { get; set; }
        public Url StartGameUrl { get; set; }
        public CashgameMatrixTableModel TableModel { get; set; }

        public CashgameMatrixPageModel(CashgameContextResult cashgameContextResult, CashgamePage selectedPage)
            : base("Cashgame Matrix", cashgameContextResult, selectedPage)
        {
        }
    }
}