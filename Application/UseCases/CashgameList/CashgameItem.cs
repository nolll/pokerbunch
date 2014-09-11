using System;
using Application.Urls;
using Core.Entities;

namespace Application.UseCases.CashgameList
{
    public class CashgameItem
    {
        public string Location { get; private set; }
        public Url Url { get; private set; }
        public int Duration { get; private set; }
        public Date Date { get; private set; }
        public int Turnover { get; private set; }
        public int AverageBuyin { get; private set; }
        public int PlayerCount { get; private set; }

        public CashgameItem(string slug, Cashgame cashgame)
        {
            Location = cashgame.Location;
            Url = new CashgameDetailsUrl(slug, cashgame.DateString);
            Duration = cashgame.Duration;
            Date = cashgame.StartTime.HasValue ? new Date(cashgame.StartTime.Value) : new Date(DateTime.MinValue);
            Turnover = cashgame.Turnover;
            AverageBuyin = cashgame.AverageBuyin;
            PlayerCount = cashgame.PlayerCount;
        }
    }
}