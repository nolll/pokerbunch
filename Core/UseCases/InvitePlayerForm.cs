using Core.Repositories;

namespace Core.UseCases
{
    public class InvitePlayerForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public InvitePlayerForm(IBunchRepository bunchRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.Get(request.PlayerId);
            var bunch = _bunchRepository.Get(player.BunchId);
            
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