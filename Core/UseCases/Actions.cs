﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Services;

namespace Core.UseCases
{
    public class Actions
    {
        private readonly ICashgameService _cashgameService;

        public Actions(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetDetailedById(request.CashgameId);
            
            var playerResult = cashgame.Players.First(o => o.Id == request.PlayerId);
            var isManager = cashgame.Role >= Role.Manager;

            var date = cashgame.StartTime;
            var playerName = playerResult.Name;
            var checkpointItems = playerResult.Actions.Select(o => CreateCheckpointItem(cashgame.Id, cashgame.Bunch, isManager, o)).ToList();

            return new Result(date, playerName, cashgame.Bunch.Id, checkpointItems);
        }

        private static CheckpointItem CreateCheckpointItem(string cashgameId, CashgameBunch bunch, bool isManager, DetailedCashgame.CashgameAction action)
        {
            var type = action.Type.ToString();
            var displayAmount = new Money(GetDisplayAmount(action), bunch.Currency);
            var time = TimeZoneInfo.ConvertTime(action.Time, bunch.Timezone);
            var canEdit = isManager;

            return new CheckpointItem(time, action.Id, cashgameId, type, displayAmount, canEdit);
        }

        private static int GetDisplayAmount(DetailedCashgame.CashgameAction action)
        {
            if (action.Type == CheckpointType.Buyin)
                return action.Added;
            return action.Stack;
        }

        public class Request
        {
            public string CashgameId { get; }
            public string PlayerId { get; }

            public Request(string cashgameId, string playerId)
            {
                CashgameId = cashgameId;
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public DateTime Date { get; }
            public string PlayerName { get; }
            public string Slug { get; }
            public IList<CheckpointItem> CheckpointItems { get; }

            public Result(DateTime date, string playerName, string slug, List<CheckpointItem> checkpointItems)
            {
                Date = date;
                PlayerName = playerName;
                Slug = slug;
                CheckpointItems = checkpointItems;
            }
        }

        public class CheckpointItem
        {
            public DateTime Time { get; }
            public string CheckpointId { get; }
            public string CashgameId { get; }
            public string Type { get; }
            public Money DisplayAmount { get; }
            public bool CanEdit { get; }

            public CheckpointItem(DateTime time, string checkpointId, string cashgameId, string type, Money displayAmount, bool canEdit)
            {
                Time = time;
                CheckpointId = checkpointId;
                CashgameId = cashgameId;
                Type = type;
                DisplayAmount = displayAmount;
                CanEdit = canEdit;
            }
        }
    }
}