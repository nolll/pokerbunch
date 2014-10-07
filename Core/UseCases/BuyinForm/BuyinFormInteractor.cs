using Core.Repositories;

namespace Core.UseCases.BuyinForm
{
    public static class BuyinFormInteractor
    {
        public static BuyinFormResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, BuyinFormRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var runningGame = cashgameRepository.GetRunning(bunch.Id);
            var canEnterStack = runningGame.IsInGame(request.PlayerId);

            return new BuyinFormResult(bunch.DefaultBuyin, canEnterStack);
        }
    }
}