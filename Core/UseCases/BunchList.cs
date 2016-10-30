using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class BunchList
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;

        public BunchList(IBunchRepository bunchRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
        }

        public Result Execute(AllBunchesRequest request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            RequireRole.Admin(user);

            var bunches = _bunchRepository.List();
            return new Result(bunches);
        }

        public Result Execute(UserBunchesRequest request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var homegames = user != null ? _bunchRepository.ListForUser() : new List<SmallBunch>();
            
            return new Result(homegames);
        }

        public class AllBunchesRequest
        {
            public string UserName { get; }

            public AllBunchesRequest(string userName)
            {
                UserName = userName;
            }
        }

        public class UserBunchesRequest
        {
            public string UserName { get; }

            public UserBunchesRequest(string userName)
            {
                UserName = userName;
            }
        }

        public class Result
        {
            public IList<ResultItem> Bunches { get; private set; }

            public Result(IEnumerable<SmallBunch> bunches)
            {
                Bunches = bunches.Select(o => new ResultItem(o)).ToList();
            }
        }

        public class ResultItem
        {
            public string Id { get; private set; }
            public string Slug { get; private set; }
            public string DisplayName { get; private set; }

            public ResultItem(SmallBunch bunch)
            {
                Id = bunch.Id;
                Slug = bunch.Id;
                DisplayName = bunch.DisplayName;
            }
        }
    }
}