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
            public Date Date { get; private set; }
            public Time Duration { get; private set; }
            public DateTime StartTime { get; private set; }
            public DateTime EndTime { get; private set; }
            public string LocationName { get; private set; }
            public string LocationId { get; private set; }
            public bool CanEdit { get; private set; }
            public string Slug { get; private set; }
            public string CashgameId { get; private set; }
            public IList<PlayerResultItem> PlayerItems { get; private set; }

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
            public string Name { get; private set; }
            public string Color { get; private set; }
            public string CashgameId { get; private set; }
            public string PlayerId { get; private set; }
            public Money Buyin { get; private set; }
            public Money Cashout { get; private set; }
            public Money Winnings { get; private set; }
            public Money WinRate { get; private set; }

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