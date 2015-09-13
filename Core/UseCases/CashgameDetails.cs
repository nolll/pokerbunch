﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class CashgameDetails
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;

        public CashgameDetails(BunchService bunchService, CashgameService cashgameService, UserService userService, PlayerService playerService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _userService = userService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetById(request.CashgameId);
            var bunch = _bunchService.Get(cashgame.BunchId);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var isManager = RoleHandler.IsInRole(user, player, Role.Manager);
            var players = GetPlayers(_playerService, cashgame);

            return new Result(bunch, cashgame, players, isManager);
        }

        private static IEnumerable<Player> GetPlayers(PlayerService playerService, Cashgame cashgame)
        {
            var playerIds = cashgame.Results.Select(o => o.PlayerId).ToList();
            return playerService.Get(playerIds);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int CashgameId { get; private set; }

            public Request(string userName, int cashgameId)
            {
                UserName = userName;
                CashgameId = cashgameId;
            }
        }

        public class Result
        {
            public Date Date { get; private set; }
            public Time Duration { get; private set; }
            public DateTime? StartTime { get; private set; }
            public DateTime? EndTime { get; private set; }
            public string Location { get; private set; }
            public bool CanEdit { get; private set; }
            public string Slug { get; private set; }
            public int CashgameId { get; private set; }
            public IList<PlayerResultItem> PlayerItems { get; private set; }

            public Result(Bunch bunch, Cashgame cashgame, IEnumerable<Player> players, bool isManager)
            {
                var sortedResults = cashgame.Results.OrderByDescending(o => o.Winnings);

                Date = Date.Parse(cashgame.DateString);
                Location = cashgame.Location;
                Duration = Time.FromMinutes(cashgame.Duration);
                StartTime = GetLocalTime(cashgame.StartTime, bunch.Timezone);
                EndTime = GetLocalTime(cashgame.EndTime, bunch.Timezone);
                CanEdit = isManager;
                Slug = bunch.Slug;
                CashgameId = cashgame.Id;
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

        public class PlayerResultItem
        {
            public string Name { get; private set; }
            public int CashgameId { get; set; }
            public int PlayerId { get; private set; }
            public Money Buyin { get; private set; }
            public Money Cashout { get; private set; }
            public MoneyResult Winnings { get; private set; }
            public MoneyWinRate WinRate { get; private set; }

            public PlayerResultItem(Bunch bunch, Cashgame cashgame, Player player, CashgameResult result)
            {
                Name = player.DisplayName;
                CashgameId = cashgame.Id;
                PlayerId = player.Id;
                Buyin = new Money(result.Buyin, bunch.Currency);
                Cashout = new Money(result.Stack, bunch.Currency);
                Winnings = new MoneyResult(result.Winnings, bunch.Currency);
                WinRate = new MoneyWinRate(result.WinRate, bunch.Currency);
            }
        }
    }
}