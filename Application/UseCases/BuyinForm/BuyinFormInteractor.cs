using Core.Repositories;

namespace Application.UseCases.BuyinForm
{
    public static class BuyinFormInteractor
    {
        public static BuyinFormResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, BuyinFormRequest request)
        {
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var runningGame = cashgameRepository.GetRunning(homegame.Id);
            var canEnterStack = runningGame.IsInGame(request.PlayerId);

            return new BuyinFormResult(homegame.DefaultBuyin, canEnterStack);
        }
    }
}