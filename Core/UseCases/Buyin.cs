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
        private readonly UserService _userService;

        public Buyin(BunchService bunchService, PlayerService playerService, CashgameService cashgameService, UserService userService)
        {
            _bunchService = bunchService;
            _playerService = playerService;
            _cashgameService = cashgameService;
            _userService = userService;
        }

        public void Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchService.Get(request.Slug);
            var currentUser = _userService.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerService.GetByUserId(bunch.Slug, currentUser.Id);
            RequireRole.Me(currentUser, currentPlayer, request.PlayerId);
            var cashgame = _cashgameService.GetRunning(bunch.Id);

            var stackAfterBuyin = request.StackAmount + request.BuyinAmount;
            var checkpoint = new BuyinCheckpoint(cashgame.Id, request.PlayerId, request.CurrentTime, stackAfterBuyin, request.BuyinAmount);
            cashgame.AddCheckpoint(checkpoint);
            _cashgameService.UpdateGame(cashgame);
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }
            public int PlayerId { get; }
            [Range(1, int.MaxValue, ErrorMessage = "Amount needs to be positive")]
            public int BuyinAmount { get; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int StackAmount { get; }
            public DateTime CurrentTime { get; }

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