using Core.Repositories;
using Core.Urls;

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
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var bunchName = bunch.DisplayName;

            var detailsUrl = new BunchDetailsUrl(request.Slug);
            
            return new Result(bunchName, detailsUrl);
        }

        public class Request
        {
            public string Slug { get; private set; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public string BunchName { get; private set; }
            public Url BunchDetailsUrl { get; private set; }

            public Result(string bunchName, Url bunchDetailsUrl)
            {
                BunchDetailsUrl = bunchDetailsUrl;
                BunchName = bunchName;
            }
        }
    }
}