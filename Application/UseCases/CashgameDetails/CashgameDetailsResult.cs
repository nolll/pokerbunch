using System;
using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Application.Urls;
using Core.Entities;

namespace Application.UseCases.CashgameDetails
{
    public class CashgameDetailsResult
    {
        public string Date { get; private set; }
        public Time Duration { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public string Location { get; private set; }
        public bool HasStartTime { get; private set; }
        public bool HasEndTime { get; private set; }
        public bool CanEdit { get; private set; }
        public bool HasCheckpoints { get; private set; }
        public bool HasDuration { get; private set; }
        public Url EditUrl { get; private set; }
        public Url CheckpointsUrl { get; private set; }
        public Url ChartDataUrl { get; private set; }
        public GameStatus Status { get; private set; }
        public IList<PlayerResultItem> PlayerItems { get; private set; }

        public CashgameDetailsResult(Homegame homegame, Cashgame cashgame, IList<Player> players, Player player, bool isManager)
        {
            var date = cashgame.StartTime.HasValue ? Globalization.FormatShortDateStatic(cashgame.StartTime.Value, true) : string.Empty;
            var showStartTime = cashgame.Status >= GameStatus.Running && cashgame.StartTime.HasValue;
            var showEndTime = cashgame.Status >= GameStatus.Finished && cashgame.EndTime != null;
            var sortedResults = cashgame.Results.OrderByDescending(o => o.Winnings);

            Date = date;
            Location = cashgame.Location;
            Duration = Time.FromMinutes(cashgame.Duration);
			HasDuration = cashgame.Duration > 0;
            HasStartTime = showStartTime;
			StartTime = showStartTime ? TimeZoneInfo.ConvertTime(cashgame.StartTime.Value, homegame.Timezone) : (DateTime?)null;
			HasEndTime = showEndTime;
            EndTime = showEndTime ? TimeZoneInfo.ConvertTime(cashgame.EndTime.Value, homegame.Timezone) : (DateTime?)null;
            Status = cashgame.Status;
            CanEdit = isManager;
            HasCheckpoints = cashgame.IsInGame(player.Id);
            EditUrl = new EditCashgameUrl(homegame.Slug, cashgame.DateString);
            CheckpointsUrl = new CashgameActionUrl(homegame.Slug, cashgame.DateString, player.Id);
            ChartDataUrl = new CashgameDetailsChartJsonUrl(homegame.Slug, cashgame.DateString);
            PlayerItems = sortedResults.Select(o => new PlayerResultItem(homegame, cashgame, GetPlayer(players, o.PlayerId), o)).ToList();
        }

        private Player GetPlayer(IList<Player> players, int playerId)
        {
            return players.First(o => o.Id == playerId);
        }
    }
}