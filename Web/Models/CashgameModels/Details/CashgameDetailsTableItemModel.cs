using Core.Services;
using Core.UseCases.CashgameDetails;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsTableItemModel
    {
        public string Name { get; private set; }
        public string PlayerUrl { get; private set; }
        public string Buyin { get; private set; }
        public string Cashout { get; private set; }
        public string Winnings { get; private set; }
        public string WinningsClass { get; private set; }
        public string Winrate { get; private set; }

        public CashgameDetailsTableItemModel(PlayerResultItem resultItem)
        {
            Name = resultItem.Name;
            PlayerUrl = resultItem.PlayerUrl.Relative;
            Buyin = resultItem.Buyin.String;
            Cashout = resultItem.Cashout.String;
            Winnings = resultItem.Winnings.String;
            WinningsClass = ResultFormatter.GetWinningsCssClass(resultItem.Winnings);
            Winrate = resultItem.WinRate.String;
        }
    }
}