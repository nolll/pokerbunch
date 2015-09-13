using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class Buyin
    {
        private readonly BunchService _bunchService;
        private readonly PlayerService _playerService;
        private readonly CashgameService _cashgameService;
        private readonly CheckpointService _checkpointService;
        private readonly UserService _userService;

        public Buyin(BunchService bunchService, PlayerService playerService, CashgameService cashgameService, CheckpointService checkpointService, UserService userService)
        {
            _bunchService = bunchService;
            _playerService = playerService;
            _cashgameService = cashgameService;
            _checkpointService = checkpointService;
            _userService = userService;
        }

        public void Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchService.GetBySlug(request.Slug);
            var currentUser = _userService.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerService.GetByUserId(bunch.Id, currentUser.Id);
            RoleHandler.RequireMe(currentUser, currentPlayer, request.PlayerId);
            var game = _cashgameService.GetRunning(bunch.Id);

            var stackAfterBuyin = request.StackAmount + request.BuyinAmount;
            var checkpoint = new BuyinCheckpoint(game.Id, request.PlayerId, request.CurrentTime, stackAfterBuyin, request.BuyinAmount);
            _checkpointService.Add(checkpoint);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }
            [Range(1, int.MaxValue, ErrorMessage = "Amount needs to be positive")]
            public int BuyinAmount { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int StackAmount { get; private set; }
            public DateTime CurrentTime { get; private set; }

            public Request(string userName, string slug, int playerId, int buyinAmount, int stackAmount, DateTime currentTime)
            {
                UserName = userName;
                Slug = slug;
                PlayerId = playerId;
                BuyinAmount = buyinAmount;
                StackAmount = stackAmount;
                CurrentTime = currentTime;
            }
        }
    }
}