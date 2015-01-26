using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.Cashout
{
    public static class CashoutInteractor
    {
        public static void Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            ICheckpointRepository checkpointRepository,
            CashoutRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var player = playerRepository.GetById(request.PlayerId);
            var cashgame = cashgameRepository.GetRunning(bunch.Id);
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
                checkpointRepository.UpdateCheckpoint(postedCheckpoint);
            else
                checkpointRepository.AddCheckpoint(postedCheckpoint);
        }

        // todo: display sharing options
        //private string GetMessage(int amount)
        //{
        //    var formattedAmount = Math.Abs(amount) + " kr";
        //    var wonOrLost = amount < 0 ? "lost" : "won";
        //    return string.Format("I just {0} {1} playing poker. #pokerbunch", wonOrLost, formattedAmount);
        //}
    }
}
