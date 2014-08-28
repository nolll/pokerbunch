using System;
using System.Collections.Generic;
using System.Linq;
using Application.Urls;
using Core.Entities;

namespace Application.UseCases.CashgameDetails
{
    public class CashgameDetailsResult
    {
        public Date Date { get; private set; }
        public Time Duration { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public string Location { get; private set; }
        public bool CanEdit { get; private set; }
        public Url EditUrl { get; private set; }
        public Url ChartDataUrl { get; private set; }
        public IList<PlayerResultItem> PlayerItems { get; private set; }

        public CashgameDetailsResult(Bunch bunch, Cashgame cashgame, IEnumerable<Player> players, bool isManager)
        {
            var sortedResults = cashgame.Results.OrderByDescending(o => o.Winnings);

            Date = Date.Parse(cashgame.DateString);
            Location = cashgame.Location;
            Duration = Time.FromMinutes(cashgame.Duration);
            StartTime = GetLocalTime(cashgame.StartTime, bunch.Timezone);
            EndTime = GetLocalTime(cashgame.EndTime, bunch.Timezone);
            CanEdit = isManager;
            EditUrl = new EditCashgameUrl(bunch.Slug, cashgame.DateString);
            ChartDataUrl = new CashgameDetailsChartJsonUrl(bunch.Slug, cashgame.DateString);
            PlayerItems = sortedResults.Select(o => new PlayerResultItem(bunch, cashgame, GetPlayer(players, o.PlayerId), o)).ToList();
        }

        private static DateTime? GetLocalTime(DateTime? d, TimeZoneInfo timeZone)
        {
            if (!d.HasValue)
                return null;
            return TimeZoneInfo.ConvertTime(d.Value, timeZone);
        }

        private static Player GetPlayer(IEnumerable<Player> players, int playerId)
        {
            return players.First(o => o.Id == playerId);
        }
    }
}