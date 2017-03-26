using Core.Services;

namespace Core.UseCases
{
    public class InvitePlayer
    {
        private readonly IPlayerService _playerService;

        public InvitePlayer(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            _playerService.Invite(request.PlayerId, request.Email);

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