using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class ClearCache
    {
        private readonly ICacheContainer _cacheContainer;
        private readonly IUserRepository _userRepository;

        public ClearCache(ICacheContainer cacheContainer, IUserRepository userRepository)
        {
            _cacheContainer = cacheContainer;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            RoleHandler.RequireAdmin(user);

            var objectCount = _cacheContainer.ClearAll();

            return new Result(objectCount);
        }

        public class Request
        {
            public string UserName { get; private set; }

            public Request(string userName)
            {
                UserName = userName;
            }
        }

        public class Result
        {
            public int DeleteCount { get; private set; }

            public Result(int deleteCount)
            {
                DeleteCount = deleteCount;
            }
        }
    }
}
