using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class BunchList
    {
        private readonly IBunchService _bunchService;

        public BunchList(IBunchService bunchService)
        {
            _bunchService = bunchService;
        }

        public Result Execute()
        {
            var bunches = _bunchService.List();
            return new Result(bunches);
        }

        public class Result
        {
            public IList<ResultItem> Bunches { get; }

            public Result(IEnumerable<SmallBunch> bunches)
            {
                Bunches = bunches.Select(o => new ResultItem(o)).ToList();
            }
        }

        public class ResultItem
        {
            public string Slug { get; }
            public string DisplayName { get; }

            public ResultItem(SmallBunch bunch)
            {
                Slug = bunch.Id;
                DisplayName = bunch.DisplayName;
            }
        }
    }
}