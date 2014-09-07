using Application.Exceptions;
using Application.Services;
using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Application.UseCases.Buyin
{
    public static class BuyinInteractor
    {
        public static BuyinResult Execute(IBunchRepository bunchRepository, IPlayerRepository playerRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository, ITimeProvider timeProvider, BuyinRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            AddCheckpoint(bunchRepository, playerRepository, cashgameRepository, checkpointRepository, timeProvider, request);
            return new BuyinResult(request.Slug);
        }

        private static void AddCheckpoint(
            IBunchRepository bunchRepository, IPlayerRepository playerRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository, ITimeProvider timeProvider, BuyinRequest request)
        {
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var player = playerRepository.GetById(request.PlayerId);
            var game = cashgameRepository.GetRunning(homegame);
            var checkpoint = CreateCheckpoint(timeProvider, request);
            checkpointRepository.AddCheckpoint(game, player, checkpoint);

            if (!game.IsStarted)
            {
                cashgameRepository.StartGame(game);
            }
        }

        private static Checkpoint CreateCheckpoint(ITimeProvider timeProvider, BuyinRequest request)
        {
            var timeStamp = timeProvider.GetTime();
            var stackAfterBuyin = request.StackAmount + request.BuyinAmount;
            return new BuyinCheckpoint(timeStamp, stackAfterBuyin, request.BuyinAmount);
        }
    }
}