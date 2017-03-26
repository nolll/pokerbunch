using Core.Services;

namespace Core.UseCases
{
    public class InvitePlayerConfirmation
    {
        private readonly IBunchService _bunchService;
        private readonly IPlayerService _playerService;

        public InvitePlayerConfirmation(IBunchService bunchService, IPlayerService playerService)
        {
            _bunchService = bunchService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var player = _playerService.Get(request.PlayerId);
            var bunch = _bunchService.Get(player.BunchId);

            return new Result(bunch.Id);
        }

        public class Request
        {
            public string PlayerId { get; }

            public Request(string playerId)
            {
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}