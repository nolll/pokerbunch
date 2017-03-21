using Core.Repositories;

namespace Core.UseCases
{
    public class InvitePlayer
    {
        private readonly IPlayerRepository _playerRepository;

        public InvitePlayer(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            _playerRepository.Invite(request.PlayerId, request.Email);

            return new Result(request.PlayerId);
        }

        public class Request
        {
            public string PlayerId { get; }
            public string Email { get; }

            public Request(string playerId, string email)
            {
                PlayerId = playerId;
                Email = email;
            }
        }

        public class Result
        {
            public string PlayerId { get; private set; }

            public Result(string playerId)
            {
                PlayerId = playerId;
            }
        }
    }
}