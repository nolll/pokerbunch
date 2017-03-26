using Core.Services;

namespace Core.UseCases
{
    public class Buyin
    {
        private readonly ICashgameService _cashgameService;

        public Buyin(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public void Execute(Request request)
        {
            var cashgame = _cashgameService.GetCurrent(request.Slug);
            _cashgameService.Buyin(cashgame.Id, request.PlayerId, request.BuyinAmount, request.StackAmount);
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