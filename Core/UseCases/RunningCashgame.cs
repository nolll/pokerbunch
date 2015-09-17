﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class RunningCashgame
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly PlayerService _playerService;
        private readonly UserService _userService;

        public RunningCashgame(BunchService bunchService, CashgameService cashgameService, PlayerService playerService, UserService userService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerService = playerService;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var cashgame = _cashgameService.GetRunning(bunch.Id);

            var x = 9;

            if(cashgame == null)
                throw new CashgameNotRunningException();

            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var players = _playerService.Get(GetPlayerIds(cashgame));
            var bunchPlayers = _playerService.GetList(bunch.Id);

            var isManager = RoleHandler.IsInRole(user, player, Role.Manager);
            
            var location = cashgame.Location;

            var playerItems = GetPlayerItems(cashgame, players);
            var bunchPlayerItems = bunchPlayers.Select(o => new BunchPlayerItem(o.Id, o.DisplayName)).OrderBy(o => o.Name).ToList();
            
            var defaultBuyin = bunch.DefaultBuyin;

            return new Result(
                bunch.Slug,
                player.Id,
                location,
                playerItems,
                bunchPlayerItems,
                defaultBuyin,
                isManager);
        }

        private static IList<int> GetPlayerIds(Cashgame cashgame)
        {
            return cashgame.Results.Select(o => o.PlayerId).ToList();
        }

        private static IList<RunningCashgamePlayerItem> GetPlayerItems(Cashgame cashgame, IList<Player> players)
        {
            var results = GetSortedResults(cashgame);
            var items = new List<RunningCashgamePlayerItem>();
            foreach (var result in results)
            {
                var playerId = result.PlayerId;
                var player = players.First(o => o.Id == playerId);
                var hasCheckedOut = result.CashoutCheckpoint != null;
                var item = new RunningCashgamePlayerItem(playerId, player.DisplayName, cashgame.Id, hasCheckedOut, result.Checkpoints);
                items.Add(item);
            }

            return items;
        }

        private static IEnumerable<CashgameResult> GetSortedResults(Cashgame cashgame)
        {
            var results = cashgame.Results;
            return results.OrderByDescending(o => o.Winnings);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }
            public string Location { get; private set; }
            public IList<RunningCashgamePlayerItem> PlayerItems { get; private set; }
            public IList<BunchPlayerItem> BunchPlayerItems { get; private set; }
            public int DefaultBuyin { get; private set; }
            public bool IsManager { get; private set; }

            public Result(
                string slug,
                int playerId,
                string location,
                IList<RunningCashgamePlayerItem> playerItems,
                IList<BunchPlayerItem> bunchPlayerItems,
                int defaultBuyin,
                bool isManager)
            {
                Slug = slug;
                PlayerId = playerId;
                Location = location;
                PlayerItems = playerItems;
                BunchPlayerItems = bunchPlayerItems;
                DefaultBuyin = defaultBuyin;
                IsManager = isManager;
            }
        }

        public class BunchPlayerItem
        {
            public int PlayerId { get; private set; }
            public string Name { get; private set; }

            public BunchPlayerItem(int playerId, string name)
            {
                PlayerId = playerId;
                Name = name;
            }
        }

        public class RunningCashgameCheckpointItem
        {
            public DateTime Time { get; private set; }
            public int Stack { get; private set; }
            public int AddedMoney { get; private set; }

            public RunningCashgameCheckpointItem(Checkpoint checkpoint)
            {
                Time = checkpoint.Timestamp;
                Stack = checkpoint.Stack;
                AddedMoney = checkpoint.Amount;
            }
        }

        public class RunningCashgamePlayerItem
        {
            public int PlayerId { get; private set; }
            public string Name { get; private set; }
            public int CashgameId { get; private set; }
            public bool HasCashedOut { get; private set; }
            public int Buyin { get; private set; }
            public int Stack { get; private set; }
            public int Winnings { get; private set; }
            public DateTime LastReport { get; set; }
            public IList<RunningCashgameCheckpointItem> Checkpoints { get; private set; }

            public RunningCashgamePlayerItem(int playerId, string name, int cashgameId, bool hasCashedOut, IEnumerable<Checkpoint> checkpoints)
            {
                PlayerId = playerId;
                Name = name;
                CashgameId = cashgameId;
                HasCashedOut = hasCashedOut;
                var list = checkpoints.ToList();
                var lastCheckpoint = list.Last();
                Checkpoints = list.Select(o => new RunningCashgameCheckpointItem(o)).ToList();
                Buyin = list.Sum(o => o.Amount);
                Stack = lastCheckpoint.Stack;
                Winnings = Stack - Buyin;
                LastReport = lastCheckpoint.Timestamp;
            }
        }
    }
}
