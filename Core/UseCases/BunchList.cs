using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class BunchList
    {
        private readonly IBunchService _bunchService;
        private readonly IUserService _userService;

        public BunchList(IBunchService bunchService, IUserService userService)
        {
            _bunchService = bunchService;
            _userService = userService;
        }

        public Result Execute()
        {
            var bunches = _bunchService.List();
            return new Result(bunches);
        }

        public Result Execute(UserBunchesRequest request)
        {
            var bunchList = GetBunchList(request.UserName);
            return new Result(bunchList);
        }

        private IList<SmallBunch> GetBunchList(string userName)
        {
            if (userName == null)
                return new List<SmallBunch>();
            return _bunchService.ListForUser();
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