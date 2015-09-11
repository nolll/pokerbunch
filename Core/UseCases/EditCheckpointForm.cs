using System;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EditCheckpointForm
    {
        private readonly BunchService _bunchService;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly IPlayerRepository _playerRepository;

        public EditCheckpointForm(BunchService bunchService, ICheckpointRepository checkpointRepository, CashgameService cashgameService, UserService userService, IPlayerRepository playerRepository)
        {
            _bunchService = bunchService;
            _checkpointRepository = checkpointRepository;
            _cashgameService = cashgameService;
            _userService = userService;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var checkpoint = _checkpointRepository.Get(request.CheckpointId);
            var cashgame = _cashgameService.GetById(checkpoint.CashgameId);
            var bunch = _bunchService.Get(cashgame.BunchId);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequireManager(user, player);
            var stack = checkpoint.Stack;
            var amount = checkpoint.Amount;
            var timestamp = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
            var canEditAmount = checkpoint.Type == CheckpointType.Buyin;

            return new Result(stack, amount, timestamp, checkpoint.Id, cashgame.Id, player.Id, bunch.Slug, canEditAmount);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int CheckpointId { get; private set; }

            public Request(string userName, int checkpointId)
            {
                UserName = userName;
                CheckpointId = checkpointId;
            }
        }

        public class Result
        {
            public int Stack { get; private set; }
            public int Amount { get; private set; }
            public DateTime TimeStamp { get; private set; }
            public int CheckpointId { get; private set; }
            public int CashgameId { get; private set; }
            public int PlayerId { get; private set; }
            public string Slug { get; private set; }
            public bool CanEditAmount { get; private set; }

            public Result(int stack, int amount, DateTime timeStamp, int checkpointId, int cashgameId, int playerId, string slug, bool canEditAmount)
            {
                TimeStamp = timeStamp;
                CheckpointId = checkpointId;
                Stack = stack;
                Amount = amount;
                CashgameId = cashgameId;
                PlayerId = playerId;
                Slug = slug;
                CanEditAmount = canEditAmount;
            }
        }
    }
}