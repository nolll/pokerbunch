using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.JoinBunchConfirmation
{
    public class JoinBunchConfirmationInteractor
    {
        private readonly IBunchRepository _bunchRepository;

        public JoinBunchConfirmationInteractor(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public JoinBunchConfirmationResult Execute(JoinBunchConfirmationRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var bunchName = bunch.DisplayName;

            var detailsUrl = new BunchDetailsUrl(request.Slug);
            
            return new JoinBunchConfirmationResult(bunchName, detailsUrl);
        }

        public class JoinBunchConfirmationRequest
        {
            public string Slug { get; private set; }

            public JoinBunchConfirmationRequest(string slug)
            {
                Slug = slug;
            }
        }

        public class JoinBunchConfirmationResult
        {
            public string BunchName { get; private set; }
            public Url BunchDetailsUrl { get; private set; }

            public JoinBunchConfirmationResult(string bunchName, Url bunchDetailsUrl)
            {
                BunchDetailsUrl = bunchDetailsUrl;
                BunchName = bunchName;
            }
        }
    }
}