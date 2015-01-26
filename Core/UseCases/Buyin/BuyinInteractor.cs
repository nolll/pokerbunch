using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases.Buyin
{
    public static class BuyinInteractor
    {
        public static void Execute(IBunchRepository bunchRepository, IPlayerRepository playerRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository, BuyinRequest request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var player = playerRepository.GetById(request.PlayerId);
            var game = cashgameRepository.GetRunning(bunch.Id);

            var stackAfterBuyin = request.StackAmount + request.BuyinAmount;
            var checkpoint = new BuyinCheckpoint(game.Id, player.Id, request.CurrentTime, stackAfterBuyin, request.BuyinAmount);
            checkpointRepository.AddCheckpoint(checkpoint);
        }
    }
}