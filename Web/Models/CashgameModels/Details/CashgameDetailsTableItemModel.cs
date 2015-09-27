using Core.Services;
using Core.UseCases;
using Web.Common.Urls.SiteUrls;

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

        public CashgameDetailsTableItemModel(CashgameDetails.PlayerResultItem resultItem)
        {
            Name = resultItem.Name;
            PlayerUrl = new CashgameActionUrl(resultItem.CashgameId, resultItem.PlayerId).Relative;
            Buyin = resultItem.Buyin.String;
            Cashout = resultItem.Cashout.String;
            Winnings = resultItem.Winnings.String;
            WinningsClass = ResultFormatter.GetWinningsCssClass(resultItem.Winnings);
            Winrate = resultItem.WinRate.String;
        }
    }
}