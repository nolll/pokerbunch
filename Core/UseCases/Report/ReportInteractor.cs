using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.Report
{
    public class ReportInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public ReportInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _checkpointRepository = checkpointRepository;
        }

        public void Execute(ReportRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetRunning(bunch.Id);
            var player = _playerRepository.GetById(request.PlayerId);

            var checkpoint = Checkpoint.Create(cashgame.Id, player.Id, request.CurrentTime, CheckpointType.Report, request.Stack);
            _checkpointRepository.AddCheckpoint(checkpoint);
        }
    }
}
