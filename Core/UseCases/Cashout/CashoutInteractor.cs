using System;
using Core.Entities.Checkpoints;
using Core.Factories;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.Cashout
{
    public static class CashoutInteractor
    {
        public static CashoutResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            ICheckpointRepository checkpointRepository,
            ITimeProvider timeProvider,
            CashoutRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var player = playerRepository.GetById(request.PlayerId);
            var cashgame = cashgameRepository.GetRunning(bunch);
            var result = cashgame.GetResult(player.Id);
            var now = timeProvider.UtcNow;

            var postedCheckpoint = CreateCheckpoint(request, result.CashoutCheckpoint, now);
            if (result.CashoutCheckpoint != null)
                checkpointRepository.UpdateCheckpoint(cashgame, postedCheckpoint);
            else
                checkpointRepository.AddCheckpoint(cashgame, player, postedCheckpoint);

            var returnUrl = new RunningCashgameUrl(request.Slug);
            return new CashoutResult(returnUrl);
        }

        private static Checkpoint CreateCheckpoint(CashoutRequest request, Checkpoint existingCashoutCheckpoint, DateTime now)
        {
            return CheckpointFactory.Create(
                now,
                CheckpointType.Cashout,
                request.Stack,
                existingCashoutCheckpoint != null ? existingCashoutCheckpoint.Id : 0);
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
