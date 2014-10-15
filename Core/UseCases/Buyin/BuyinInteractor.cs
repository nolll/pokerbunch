using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Core.UseCases.Buyin
{
    public static class BuyinInteractor
    {
        public static BuyinResult Execute(IBunchRepository bunchRepository, IPlayerRepository playerRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository, ITimeProvider timeProvider, BuyinRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var player = playerRepository.GetById(request.PlayerId);
            var game = cashgameRepository.GetRunning(bunch);
            var checkpoint = CreateCheckpoint(timeProvider, request);
            checkpointRepository.AddCheckpoint(game, player, checkpoint);

            if (!game.IsStarted)
            {
                cashgameRepository.StartGame(game);
            }

            return new BuyinResult(request.Slug);
        }

        private static Checkpoint CreateCheckpoint(ITimeProvider timeProvider, BuyinRequest request)
        {
            var timeStamp = timeProvider.UtcNow;
            var stackAfterBuyin = request.StackAmount + request.BuyinAmount;
            return new BuyinCheckpoint(timeStamp, stackAfterBuyin, request.BuyinAmount);
        }
    }
}