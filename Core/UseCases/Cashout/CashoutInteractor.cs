using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.Cashout
{
    public class CashoutInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public CashoutInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _checkpointRepository = checkpointRepository;
        }

        public void Execute(CashoutRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var player = _playerRepository.GetById(request.PlayerId);
            var cashgame = _cashgameRepository.GetRunning(bunch.Id);
            var result = cashgame.GetResult(player.Id);

            var existingCashoutCheckpoint = result.CashoutCheckpoint;
            var postedCheckpoint = Checkpoint.Create(
                cashgame.Id,
                player.Id,
                request.CurrentTime,
                CheckpointType.Cashout,
                request.Stack,
                0,
                existingCashoutCheckpoint != null ? existingCashoutCheckpoint.Id : 0);

            if (existingCashoutCheckpoint != null)
                _checkpointRepository.UpdateCheckpoint(postedCheckpoint);
            else
                _checkpointRepository.AddCheckpoint(postedCheckpoint);
        }
    }
}
