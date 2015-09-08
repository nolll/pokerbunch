using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class Actions
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly UserService _userService;

        public Actions(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, UserService userService)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.GetById(request.PlayerId);
            var user = _userService.GetByNameOrEmail(request.CurrentUserName);
            var bunch = _bunchRepository.GetById(player.BunchId);
            var cashgame = _cashgameRepository.GetById(request.CashgameId);
            
            RoleHandler.RequirePlayer(user, player);
            var playerResult = cashgame.GetResult(player.Id);
            var currentPlayer = _playerRepository.GetByUserId(bunch.Id, user.Id);
            var isManager = RoleHandler.IsInRole(user, currentPlayer, Role.Manager);

            var date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.MinValue;
            var playerName = player.DisplayName;
            var checkpointItems = playerResult.Checkpoints.Select(o => CreateCheckpointItem(bunch, isManager, o)).ToList();

            return new Result(date, playerName, bunch.Slug, checkpointItems);
        }

        private static CheckpointItem CreateCheckpointItem(Bunch bunch, bool isManager, Checkpoint checkpoint)
        {
            var type = checkpoint.Description;
            var displayAmount = new Money(GetDisplayAmount(checkpoint), bunch.Currency);
            var time = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
            var canEdit = isManager;

            return new CheckpointItem(time, checkpoint.Id, type, displayAmount, canEdit);
        }

        private static int GetDisplayAmount(Checkpoint checkpoint)
        {
            if (checkpoint.Type == CheckpointType.Buyin)
                return checkpoint.Amount;
            return checkpoint.Stack;
        }

        public class Request
        {
            public string CurrentUserName { get; private set; }
            public int CashgameId { get; private set; }
            public int PlayerId { get; private set; }

            public Request(string currentUserName, int cashgameId, int playerId)
            {
                CurrentUserName = currentUserName;
                CashgameId = cashgameId;
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public DateTime Date { get; private set; }
            public string PlayerName { get; private set; }
            public string Slug { get; private set; }
            public IList<CheckpointItem> CheckpointItems { get; private set; }

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
            public DateTime Time { get; private set; }
            public int CheckpointId { get; private set; }
            public string Type { get; private set; }
            public Money DisplayAmount { get; private set; }
            public bool CanEdit { get; private set; }

            public CheckpointItem(DateTime time, int checkpointId, string type, Money displayAmount, bool canEdit)
            {
                Time = time;
                CheckpointId = checkpointId;
                Type = type;
                DisplayAmount = displayAmount;
                CanEdit = canEdit;
            }
        }
    }
}