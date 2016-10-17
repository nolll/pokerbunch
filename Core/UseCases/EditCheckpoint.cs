﻿using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditCheckpoint
    {
        private readonly BunchService _bunchService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly CashgameService _cashgameService;

        public EditCheckpoint(BunchService bunchService, UserService userService, PlayerService playerService, CashgameService cashgameService)
        {
            _bunchService = bunchService;
            _userService = userService;
            _playerService = playerService;
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var cashgame = _cashgameService.GetByCheckpoint(request.CheckpointId);
            var existingCheckpoint = cashgame.GetCheckpoint(request.CheckpointId);
            var bunch = _bunchService.Get(cashgame.Bunch);
            var currentUser = _userService.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerService.GetByUserId(bunch.Slug, currentUser.Id);
            RequireRole.Manager(currentUser, currentPlayer);
            
            var postedCheckpoint = Checkpoint.Create(
                existingCheckpoint.CashgameId,
                existingCheckpoint.PlayerId,
                TimeZoneInfo.ConvertTimeToUtc(request.Timestamp, bunch.Timezone),
                existingCheckpoint.Type,
                request.Stack,
                request.Amount,
                existingCheckpoint.Id);

            cashgame.UpdateCheckpoint(postedCheckpoint);
            _cashgameService.UpdateGame(cashgame);

            return new Result(cashgame.Id, existingCheckpoint.PlayerId);
        }

        public class Request
        {
            public string UserName { get; }
            public string CheckpointId { get; }
            public DateTime Timestamp { get; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int Stack { get; }
            [Range(0, int.MaxValue, ErrorMessage = "Amount can't be negative")]
            public int Amount { get; }

            public Request(string userName, string checkpointId, DateTime timestamp, int stack, int amount)
            {
                UserName = userName;
                CheckpointId = checkpointId;
                Timestamp = timestamp;
                Stack = stack;
                Amount = amount;
            }
        }

        public class Result
        {
            public string CashgameId { get; private set; }
            public string PlayerId { get; private set; }

            public Result(string cashgameId, string playerId)
            {
                CashgameId = cashgameId;
                PlayerId = playerId;
            }
        }
    }
}
