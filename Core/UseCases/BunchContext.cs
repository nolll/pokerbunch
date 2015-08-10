using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases
{
    public class BunchContext
    {
        private readonly IUserRepository _userRepository;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public BunchContext(IUserRepository userRepository, IBunchRepository bunchRepository, IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var appContext = new AppContext(_userRepository).Execute(new AppContext.Request(request.UserName));

            var bunch = GetBunch(appContext, request);
            if(bunch == null)
                return new Result(appContext);

            var player = _playerRepository.GetByUserId(bunch.Id, appContext.UserId);
            var role = appContext.IsAdmin ? Role.Admin : player.Role;

            return new Result(appContext, bunch.Slug, bunch.Id, bunch.DisplayName, role, player.Id);
        }

        private Bunch GetBunch(AppContext.Result appContext, Request request)
        {
            if (!appContext.IsLoggedIn)
                return null;

            if (request.HasPlayerId)
            {
                var player = _playerRepository.GetById(request.PlayerId);
                return _bunchRepository.GetById(player.BunchId);
            }

            if (request.HasSlug)
            {
                try
                {
                    return _bunchRepository.GetBySlug(request.Slug);
                }
                catch (BunchNotFoundException)
                {
                    return null;
                }
            }
            var bunches = _bunchRepository.GetByUserId(appContext.UserId);
            return bunches.Count == 1 ? bunches[0] : null;
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }

            public Request(string userName, string slug = null)
            {
                UserName = userName;
                Slug = slug;
            }

            public Request(string userName, int playerId)
            {
                UserName = userName;
                PlayerId = playerId;
            }

            public bool HasSlug
            {
                get { return !string.IsNullOrEmpty(Slug); }
            }

            public bool HasPlayerId
            {
                get { return PlayerId > 0; }
            }
        }

        public class Result
        {
            private readonly Role _userRole;
            private readonly int _userPlayerId;

            public int BunchId { get; private set; }
            public string Slug { get; private set; }
            public string BunchName { get; private set; }
            public bool HasBunch { get; private set; }
            public Url BunchUrl { get; private set; }
            public Url CashgameUrl { get; private set; }
            public Url EventUrl { get; private set; }
            public AppContext.Result AppContext { get; private set; }

            public Result(AppContext.Result appContextResult)
            {
                AppContext = appContextResult;
            }

            public Result(AppContext.Result appContextResult, string slug, int bunchId, string bunchName, Role userRole, int userPlayerId)
                : this(appContextResult)
            {
                _userRole = userRole;
                _userPlayerId = userPlayerId;

                BunchId = bunchId;
                Slug = slug;
                BunchName = bunchName;
                HasBunch = true;
                BunchUrl = new BunchDetailsUrl(slug);
                CashgameUrl = new CashgameIndexUrl(slug);
                EventUrl = new EventListUrl(slug);
            }

            public bool IsAdmin
            {
                get { return IsInRole(Role.Admin); }
            }

            public bool IsManager
            {
                get { return IsInRole(Role.Manager); }
            }

            public bool IsPlayer
            {
                get { return IsInRole(Role.Player); }
            }

            public bool IsCurrentPlayer(int playerId)
            {
                return IsAdmin || playerId == _userPlayerId;
            }

            private bool IsInRole(Role requestedRole)
            {
                return requestedRole <= _userRole;
            }
        }
    }
}