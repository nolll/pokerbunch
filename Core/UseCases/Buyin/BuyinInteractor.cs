using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.Buyin
{
    public class BuyinInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public BuyinInteractor(IBunchRepository bunchRepository, IPlayerRepository playerRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
        }

        public void Execute(BuyinRequest request)
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
    }
}