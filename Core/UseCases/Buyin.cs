using Core.Repositories;

namespace Core.UseCases
{
    public class Buyin
    {
        private readonly ICashgameRepository _cashgameRepository;

        public Buyin(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public void Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetRunning(request.Slug);
            _cashgameRepository.Buyin(cashgame.Id, request.PlayerId, request.BuyinAmount, request.StackAmount);
        }

        public class Request
        {
            public string Slug { get; }
            public string PlayerId { get; }
            public int BuyinAmount { get; }
            public int StackAmount { get; }

            public Request(string slug, string playerId, int buyinAmount, int stackAmount)
            {
                Slug = slug;
                PlayerId = playerId;
                BuyinAmount = buyinAmount;
                StackAmount = stackAmount;
            }
        }
    }
}