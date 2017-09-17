using Core.Services;

namespace Core.UseCases
{
    public class JoinBunchConfirmation
    {
        private readonly IBunchService _bunchService;

        public JoinBunchConfirmation(IBunchService bunchService)
        {
            _bunchService = bunchService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.BunchId);
            var bunchName = bunch.DisplayName;

            return new Result(bunch.Id, bunchName);
        }

        public class Request
        {
            public string BunchId { get; }

            public Request(string bunchId)
            {
                BunchId = bunchId;
            }
        }

        public class Result
        {
            public string BunchId { get; }
            public string BunchName { get; }

            public Result(string bunchId, string bunchName)
            {
                BunchId = bunchId;
                BunchName = bunchName;
            }
        }
    }
}