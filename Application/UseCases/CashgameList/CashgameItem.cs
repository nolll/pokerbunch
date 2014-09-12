using System;
using Application.Urls;
using Core.Entities;

namespace Application.UseCases.CashgameList
{
    public class CashgameItem
    {
        public string Location { get; private set; }
        public Url Url { get; private set; }
        public Time Duration { get; private set; }
        public Date Date { get; private set; }
        public Money Turnover { get; private set; }
        public Money AverageBuyin { get; private set; }
        public int PlayerCount { get; private set; }

        public CashgameItem(Bunch bunch, Cashgame cashgame)
        {
            Location = cashgame.Location;
            Url = new CashgameDetailsUrl(bunch.Slug, cashgame.DateString);
            Duration = Time.FromMinutes(cashgame.Duration);
            Date = cashgame.StartTime.HasValue ? new Date(cashgame.StartTime.Value) : new Date(DateTime.MinValue);
            Turnover = new Money(cashgame.Turnover, bunch.Currency);
            AverageBuyin = new Money(cashgame.AverageBuyin, bunch.Currency);
            PlayerCount = cashgame.PlayerCount;
        }
    }
}