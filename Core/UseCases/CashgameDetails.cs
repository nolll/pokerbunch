using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class CashgameDetails
    {
        private readonly ICashgameService _cashgameService;

        public CashgameDetails(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetDetailedById(request.CashgameId);

            if (cashgame.IsRunning)
                throw new CashgameRunningException();

            return new Result(cashgame);
        }

        public class Request
        {
            public string CashgameId { get; }

            public Request(string cashgameId)
            {
                CashgameId = cashgameId;
            }
        }

        public class Result
        {
            public Date Date { get; }
            public Time Duration { get; }
            public DateTime StartTime { get; }
            public DateTime EndTime { get; }
            public string LocationName { get; }
            public string LocationId { get; }
            public bool CanEdit { get; }
            public string Slug { get; }
            public string CashgameId { get; }
            public IList<PlayerResultItem> PlayerItems { get; }

            public Result(DetailedCashgame cashgame)
            {
                var sortedResults = cashgame.Players.OrderByDescending(o => o.Winnings);

                var timezone = cashgame.Bunch.Timezone;
                var startTime = TimeZoneInfo.ConvertTime(cashgame.StartTime, timezone);
                var endTime = TimeZoneInfo.ConvertTime(cashgame.UpdatedTime, timezone);
                var duration = endTime - startTime;

                Date = new Date(startTime);
                LocationName = cashgame.Location.Name;
                LocationId = cashgame.Location.Id;
                Duration = Time.FromTimespan(duration);
                StartTime = startTime;
                EndTime = endTime;
                CanEdit = RoleHandler.IsInRole(cashgame.Role, Role.Manager);
                Slug = cashgame.Bunch.Id;
                CashgameId = cashgame.Id;
                PlayerItems = sortedResults.Select(o => new PlayerResultItem(cashgame, o)).ToList();
            }
        }

        public class PlayerResultItem
        {
            public string Name { get; }
            public string Color { get; }
            public string CashgameId { get; }
            public string PlayerId { get; }
            public Money Buyin { get; }
            public Money Cashout { get; }
            public Money Winnings { get; }
            public Money WinRate { get; }

            public PlayerResultItem(DetailedCashgame cashgame, DetailedCashgame.CashgamePlayer player)
            {
                var currency = cashgame.Bunch.Currency;

                Name = player.Name;
                Color = player.Color;
                CashgameId = cashgame.Id;
                PlayerId = player.Id;
                Buyin = new Money(player.Buyin, currency);
                Cashout = new Money(player.Stack, currency);
                Winnings = new Money(player.Winnings, currency);
                WinRate = new Money(player.Winrate, currency);
            }
        }
    }
}