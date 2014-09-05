using Application.Exceptions;
using Application.Services;
using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Application.UseCases.Buyin
{
    public class BuyinInteractor : IBuyinInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ITimeProvider _timeProvider;

        public BuyinInteractor(
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            ICheckpointRepository checkpointRepository,
            ITimeProvider timeProvider)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
            _timeProvider = timeProvider;
        }

        public BuyinResult Execute(BuyinRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            AddCheckpoint(request);
            return new BuyinResult(request.Slug);
        }

        private void AddCheckpoint(BuyinRequest request)
        {
            var homegame = _bunchRepository.GetBySlug(request.Slug);
            var player = _playerRepository.GetById(request.PlayerId);
            var game = _cashgameRepository.GetRunning(homegame);
            var checkpoint = CreateCheckpoint(request);
            _checkpointRepository.AddCheckpoint(game, player, checkpoint);

            if (!game.IsStarted)
            {
                _cashgameRepository.StartGame(game);
            }
        }

        private Checkpoint CreateCheckpoint(BuyinRequest request)
        {
            var timeStamp = _timeProvider.GetTime();
            var stackAfterBuyin = request.StackAmount + request.BuyinAmount;
            return new BuyinCheckpoint(timeStamp, stackAfterBuyin, request.BuyinAmount);
        }
    }
}