using Application.Services;
using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Application.UseCases.Buyin
{
    public class BuyinInteractor : IBuyinInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ITimeProvider _timeProvider;

        public BuyinInteractor(
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            ICheckpointRepository checkpointRepository,
            ITimeProvider timeProvider)
        {
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
            _timeProvider = timeProvider;
        }

        public BuyinResult Execute(BuyinRequest request)
        {
            var validator = new Validator(request);

            if (validator.IsValid)
                AddCheckpoint(request);

            return new BuyinResult(request.Slug, validator);
        }

        private void AddCheckpoint(BuyinRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
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