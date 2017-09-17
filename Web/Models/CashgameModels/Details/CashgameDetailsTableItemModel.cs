using Core.Services;
using Core.UseCases;
using Web.Extensions;
using Web.Services;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Details
{
    public class CashgameDetailsTableItemModel : IViewModel
    {
        public string Name { get; }
        public string Color { get; }
        public string PlayerUrl { get; }
        public string Buyin { get; }
        public string Cashout { get; }
        public string Winnings { get; }
        public string WinningsClass { get; }
        public string Winrate { get; }

        public CashgameDetailsTableItemModel(CashgameDetails.PlayerResultItem resultItem)
        {
            Name = resultItem.Name;
            Color = resultItem.Color;
            PlayerUrl = new CashgameActionUrl(resultItem.CashgameId, resultItem.PlayerId).Relative;
            Buyin = resultItem.Buyin.ToString();
            Cashout = resultItem.Cashout.ToString();
            Winnings = ResultFormatter.FormatWinnings(resultItem.Winnings);
            WinningsClass = CssService.GetWinningsCssClass(resultItem.Winnings);
            Winrate = ResultFormatter.FormatWinRate(resultItem.WinRate);
        }

        public View GetView()
        {
            return new View("CashgameDetails/ResultTableItem");
        }
    }
}