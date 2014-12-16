using Core.Entities;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.DeleteCheckpoint
{
    public static class DeleteCheckpointInteractor
    {
        public static DeleteCheckpointResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            ICheckpointRepository checkpointRepository,
            DeleteCheckpointRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            var checkpoint = checkpointRepository.GetCheckpoint(request.CheckpointId);
            checkpointRepository.DeleteCheckpoint(checkpoint);

            var returnUrl = GetReturnUrl(cashgame.Status, request);
            return new DeleteCheckpointResult(returnUrl);
        }

        private static Url GetReturnUrl(GameStatus status, DeleteCheckpointRequest request)
        {
            if(status == GameStatus.Running)
                return new RunningCashgameUrl(request.Slug);
            return new CashgameDetailsUrl(request.Slug, request.DateStr);
        }
    }
}
