using Application.Services;
using Application.UseCases.CashgameDetails;

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
            Buyin = resultItem.Buyin.ToString();
            Cashout = resultItem.Cashout.ToString();
            Winnings = resultItem.Winnings.ToString();
            WinningsClass = ResultFormatter.GetWinningsCssClass(resultItem.Winnings);
            Winrate = resultItem.Winrate.ToString();
        }
    }
}