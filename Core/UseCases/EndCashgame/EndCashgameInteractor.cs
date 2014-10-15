using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.EndCashgame
{
    public static class EndCashgameInteractor
    {
        public static EndCashgameResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, EndCashgameRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetRunning(bunch);

            if(cashgame != null)
                cashgameRepository.EndGame(bunch, cashgame);

            var returnUrl = new CashgameIndexUrl(request.Slug);

            return new EndCashgameResult(returnUrl);
        }
    }

    public class EndCashgameRequest
    {
        public string Slug { get; private set; }

        public EndCashgameRequest(string slug)
        {
            Slug = slug;
        }
    }

    public class EndCashgameResult
    {
        public Url ReturnUrl { get; private set; }

        public EndCashgameResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}
