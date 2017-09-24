using Core.UseCases;
using JetBrains.Annotations;
using PokerBunch.Common.Urls.SiteUrls;

namespace Web.Models.CashgameModels.List
{
    public class CashgameListTableItemJsonModel
    {
        [UsedImplicitly]
        public string Date { get; private set; }

        [UsedImplicitly]
        public int PlayerCount { get; private set; }

        [UsedImplicitly]
        public int Duration { get; private set; }

        [UsedImplicitly]
        public int Turnover { get; private set; }

        [UsedImplicitly]
        public int AverageBuyin { get; private set; }

        [UsedImplicitly]
        public string Url { get; private set; }

        [UsedImplicitly]
        public string Location { get; private set; }

        public CashgameListTableItemJsonModel(CashgameList.Item item)
        {
            Date = item.Date.IsoString;
            PlayerCount = item.PlayerCount;
            Duration = item.Duration.Minutes;
            Turnover = item.Turnover.Amount;
            AverageBuyin = item.AverageBuyin.Amount;
            Url = new CashgameDetailsUrl(item.CashgameId).Relative;
            Location = item.Location;
        }
    }
}