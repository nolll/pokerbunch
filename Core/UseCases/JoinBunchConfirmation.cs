using Core.Repositories;

namespace Core.UseCases
{
    public class JoinBunchConfirmation
    {
        private readonly IBunchRepository _bunchRepository;

        public JoinBunchConfirmation(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.BunchId);
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
            public string BunchId { get; private set; }
            public string BunchName { get; private set; }

            public Result(string bunchId, string bunchName)
            {
                BunchId = bunchId;
                BunchName = bunchName;
            }
        }
    }
}