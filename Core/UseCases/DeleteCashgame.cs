using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases
{
    public class DeleteCashgame
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public DeleteCashgame(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            
            if (cashgame.PlayerCount > 0)
                throw new CashgameHasResultsException();

            _cashgameRepository.DeleteGame(cashgame);

            var returnUrl = new CashgameIndexUrl(request.Slug);
            return new Result(returnUrl);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public string DateStr { get; private set; }

            public Request(string slug, string dateStr)
            {
                Slug = slug;
                DateStr = dateStr;
            }
        }

        public class Result
        {
            public Url ReturnUrl { get; private set; }

            public Result(Url returnUrl)
            {
                ReturnUrl = returnUrl;
            }
        }
    }
}
