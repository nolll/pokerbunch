using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class BunchContext
    {
        private readonly IUserRepository _userRepository;
        private readonly IBunchRepository _bunchRepository;

        public BunchContext(IUserRepository userRepository, IBunchRepository bunchRepository)
        {
            _userRepository = userRepository;
            _bunchRepository = bunchRepository;
        }

        public Result Execute(BunchRequest request)
        {
            var appContext = new CoreContext(_userRepository).Execute(new CoreContext.Request(request.UserName));
            var bunch = GetBunch(appContext, request);
            return GetResult(appContext, bunch);
        }

        private Result GetResult(CoreContext.Result appContext, SmallBunch bunch)
        {
            if (bunch == null)
                return new Result(appContext);

            return new Result(appContext, bunch.Id, bunch.Id, bunch.DisplayName);
        }

        private SmallBunch GetBunch(CoreContext.Result appContext, BunchRequest request)
        {
            if (!appContext.IsLoggedIn)
                return null;

            if (!string.IsNullOrEmpty(request.BunchId))
            {
                try
                {
                    return _bunchRepository.Get(request.BunchId);
                }
                catch (BunchNotFoundException)
                {
                    return null;
                }
            }
            var bunches = _bunchRepository.ListForUser();
            return bunches.Count == 1 ? bunches[0] : null;
        }

        public class BunchRequest
        {
            public string UserName { get; }
            public string BunchId { get; }

            public BunchRequest(string userName, string bunchId = null)
            {
                UserName = userName;
                BunchId = bunchId;
            }
        }

        public class Result
        {
            public string BunchId { get; private set; }
            public string Slug { get; private set; }
            public string BunchName { get; private set; }
            public bool HasBunch { get; private set; }
            public CoreContext.Result AppContext { get; private set; }

            public Result(CoreContext.Result appContextResult)
            {
                AppContext = appContextResult;
            }

            public Result(CoreContext.Result appContextResult, string slug, string bunchId, string bunchName)
                : this(appContextResult)
            {
                BunchId = bunchId;
                Slug = slug;
                BunchName = bunchName;
                HasBunch = true;
            }
        }
    }
}