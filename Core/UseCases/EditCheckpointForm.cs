using System;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EditCheckpointForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public EditCheckpointForm(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetByCheckpoint(request.CheckpointId);
            var checkpoint = cashgame.GetCheckpoint(request.CheckpointId);
            var bunch = _bunchRepository.Get(cashgame.BunchId);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUser(bunch.Id, user.Id);
            RequireRole.Manager(user, player);
            var stack = checkpoint.Stack;
            var amount = checkpoint.Amount;
            var timestamp = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
            var canEditAmount = checkpoint.Type == CheckpointType.Buyin;

            return new Result(stack, amount, timestamp, checkpoint.Id, cashgame.Id, player.Id, bunch.Id, canEditAmount);
        }

        public class Request
        {
            public string UserName { get; }
            public string CheckpointId { get; }

            public Request(string userName, string checkpointId)
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
            public string CheckpointId { get; private set; }
            public string CashgameId { get; private set; }
            public string PlayerId { get; private set; }
            public string Slug { get; private set; }
            public bool CanEditAmount { get; private set; }

            public Result(int stack, int amount, DateTime timeStamp, string checkpointId, string cashgameId, string playerId, string slug, bool canEditAmount)
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