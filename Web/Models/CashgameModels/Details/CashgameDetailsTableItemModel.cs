using Core.Services;
using Core.UseCases;
using Web.Services;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsTableItemModel
    {
        public string Name { get; private set; }
        public string Color { get; private set; }
        public string PlayerUrl { get; private set; }
        public string Buyin { get; private set; }
        public string Cashout { get; private set; }
        public string Winnings { get; private set; }
        public string WinningsClass { get; private set; }
        //public string Winrate { get; private set; }

        public CashgameDetailsTableItemModel(CashgameDetails.PlayerResultItem resultItem)
        {
            Name = resultItem.Name;
            Color = resultItem.Color;
            PlayerUrl = new CashgameActionUrl(resultItem.CashgameId, resultItem.PlayerId).Relative;
            Buyin = resultItem.Buyin.ToString();
            Cashout = resultItem.Cashout.ToString();
            Winnings = ResultFormatter.FormatWinnings(resultItem.Winnings);
            WinningsClass = CssService.GetWinningsCssClass(resultItem.Winnings);
            //Winrate = ResultFormatter.FormatWinRate(resultItem.WinRate);
        }
    }
}