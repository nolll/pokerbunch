using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases
{
    public class DeleteCashgame
    {
        private readonly ICashgameRepository _cashgameRepository;

        public DeleteCashgame(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetById(request.Id);
            
            if (cashgame.PlayerCount > 0)
                throw new CashgameHasResultsException();

            _cashgameRepository.DeleteGame(cashgame);

            var returnUrl = new CashgameIndexUrl(request.Slug);
            return new Result(returnUrl);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public int Id { get; private set; }

            public Request(string slug, int id)
            {
                Slug = slug;
                Id = id;
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
