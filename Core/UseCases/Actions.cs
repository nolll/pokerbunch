using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class Actions
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;

        public Actions(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
        }

        public Result Execute(Request input)
        {
            var user = _userRepository.GetByNameOrEmail(input.CurrentUserName);
            var bunch = _bunchRepository.GetBySlug(input.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, input.DateStr);
            var player = _playerRepository.GetById(input.PlayerId);
            RoleHandler.RequirePlayer(user, player);
            var playerResult = cashgame.GetResult(player.Id);
            var currentPlayer = _playerRepository.GetByUserId(bunch.Id, user.Id);
            var isManager = RoleHandler.IsInRole(user, currentPlayer, Role.Manager);

            var date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.MinValue;
            var playerName = player.DisplayName;
            var checkpointItems = playerResult.Checkpoints.Select(o => CreateCheckpointItem(bunch, cashgame, player, isManager, o)).ToList();

            return new Result(date, playerName, checkpointItems);
        }

        private static CheckpointItem CreateCheckpointItem(Bunch bunch, Cashgame cashgame, Player player, bool isManager, Checkpoint checkpoint)
        {
            var type = checkpoint.Description;
            var displayAmount = new Money(GetDisplayAmount(checkpoint), bunch.Currency);
            var time = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
            var canEdit = isManager;
            var editUrl = new EditCheckpointUrl(bunch.Slug, cashgame.DateString, player.Id, checkpoint.Id);

            return new CheckpointItem(time, editUrl, type, displayAmount, canEdit);
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
            public string Slug { get; private set; }
            public string DateStr { get; private set; }
            public int PlayerId { get; private set; }

            public Request(string currentUserName, string slug, string dateStr, int playerId)
            {
                CurrentUserName = currentUserName;
                Slug = slug;
                DateStr = dateStr;
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public DateTime Date { get; private set; }
            public string PlayerName { get; private set; }
            public IList<CheckpointItem> CheckpointItems { get; private set; }

            public Result(DateTime date, string playerName, List<CheckpointItem> checkpointItems)
            {
                Date = date;
                PlayerName = playerName;
                CheckpointItems = checkpointItems;
            }
        }

        public class CheckpointItem
        {
            public DateTime Time { get; private set; }
            public Url EditUrl { get; private set; }
            public string Type { get; private set; }
            public Money DisplayAmount { get; private set; }
            public bool CanEdit { get; private set; }

            public CheckpointItem(DateTime time, Url editUrl, string type, Money displayAmount, bool canEdit)
            {
                Time = time;
                EditUrl = editUrl;
                Type = type;
                DisplayAmount = displayAmount;
                CanEdit = canEdit;
            }
        }
    }
}