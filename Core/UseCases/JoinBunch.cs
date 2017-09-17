using Core.Services;

namespace Core.UseCases
{
    public class JoinBunch
    {
        private readonly IBunchService _bunchService;

        public JoinBunch(IBunchService bunchService)
        {
            _bunchService = bunchService;
        }

        public Result Execute(Request request)
        {
            _bunchService.Join(request.BunchId, request.Code);
            return new Result(request.BunchId);
        }
        
        public class Request
        {
            public string BunchId { get; }
            public string Code { get; }

            public Request(string bunchId, string code)
            {
                BunchId = bunchId;
                Code = code;
            }
        }

        public class Result
        {
            public string BunchId { get; }

            public Result(string bunchId)
            {
                BunchId = bunchId;
            }
        }
    }
}
