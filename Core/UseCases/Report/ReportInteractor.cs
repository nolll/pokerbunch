using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.Report
{
    public static class ReportInteractor
    {
        public static void Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            ICheckpointRepository checkpointRepository,
            ReportRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetRunning(bunch.Id);
            var player = playerRepository.GetById(request.PlayerId);

            var checkpoint = Checkpoint.Create(cashgame.Id, player.Id, request.CurrentTime, CheckpointType.Report, request.Stack);
            checkpointRepository.AddCheckpoint(checkpoint);
        }
    }
}
