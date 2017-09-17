using Core.Services;

namespace Core.UseCases
{
    public class JoinBunchForm
    {
        private readonly IBunchService _bunchService;

        public JoinBunchForm(IBunchService bunchService)
        {
            _bunchService = bunchService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.Slug);

            return new Result(bunch.DisplayName);
        }

        public class Request
        {
            public string Slug { get; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public string BunchName { get; }

            public Result(string bunchName)
            {
                BunchName = bunchName;
            }
        }
    }
}