using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class JoinBunchConfirmation
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public JoinBunchConfirmation(IBunchRepository bunchRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var bunchName = bunch.DisplayName;

            var detailsUrl = new BunchDetailsUrl(request.Slug);
            
            return new Result(bunchName, detailsUrl);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }

        public class Result
        {
            public string BunchName { get; private set; }
            public Url BunchDetailsUrl { get; private set; }

            public Result(string bunchName, Url bunchDetailsUrl)
            {
                BunchDetailsUrl = bunchDetailsUrl;
                BunchName = bunchName;
            }
        }
    }
}