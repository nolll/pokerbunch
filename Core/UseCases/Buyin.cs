using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Checkpoints;
using Core.Repositories;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class Buyin
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public Buyin(IBunchRepository bunchRepository, IPlayerRepository playerRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
        }

        public void Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var player = _playerRepository.GetById(request.PlayerId);
            var game = _cashgameRepository.GetRunning(bunch.Id);

            var stackAfterBuyin = request.StackAmount + request.BuyinAmount;
            var checkpoint = new BuyinCheckpoint(game.Id, player.Id, request.CurrentTime, stackAfterBuyin, request.BuyinAmount);
            _checkpointRepository.AddCheckpoint(checkpoint);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }
            [Range(1, int.MaxValue, ErrorMessage = "Amount needs to be positive")]
            public int BuyinAmount { get; private set; }
            [Range(0, int.MaxValue, ErrorMessage = "Stack can't be negative")]
            public int StackAmount { get; private set; }
            public DateTime CurrentTime { get; private set; }

            public Request(string slug, int playerId, int buyinAmount, int stackAmount, DateTime currentTime)
            {
                Slug = slug;
                PlayerId = playerId;
                BuyinAmount = buyinAmount;
                StackAmount = stackAmount;
                CurrentTime = currentTime;
            }
        }
    }
}