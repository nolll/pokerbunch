using System;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class EditCheckpointForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public EditCheckpointForm(IBunchRepository bunchRepository, ICheckpointRepository checkpointRepository, ICashgameRepository cashgameRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _checkpointRepository = checkpointRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var checkpoint = _checkpointRepository.GetCheckpoint(request.CheckpointId);
            var cashgame = _cashgameRepository.GetById(checkpoint.CashgameId);
            var bunch = _bunchRepository.GetById(cashgame.BunchId);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequireManager(user, player);
            var stack = checkpoint.Stack;
            var amount = checkpoint.Amount;
            var timestamp = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, bunch.Timezone);
            var deleteUrl = new DeleteCheckpointUrl(request.CheckpointId);
            var cancelUrl = new CashgameActionUrl(bunch.Slug, cashgame.DateString, checkpoint.PlayerId);
            var canEditAmount = checkpoint.Type == CheckpointType.Buyin;

            return new Result(stack, amount, timestamp, deleteUrl, cancelUrl, bunch.Slug, canEditAmount);
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
            public Url DeleteUrl { get; private set; }
            public Url CancelUrl { get; private set; }
            public string Slug { get; private set; }
            public bool CanEditAmount { get; private set; }

            public Result(int stack, int amount, DateTime timeStamp, Url deleteUrl, Url cancelUrl, string slug, bool canEditAmount)
            {
                TimeStamp = timeStamp;
                Stack = stack;
                Amount = amount;
                DeleteUrl = deleteUrl;
                CancelUrl = cancelUrl;
                Slug = slug;
                CanEditAmount = canEditAmount;
            }
        }
    }
}