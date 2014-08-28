using Application.Urls;
using Core.Repositories;

namespace Application.UseCases.JoinBunchConfirmation
{
    public class JoinBunchConfirmationInteractor : IJoinBunchConfirmationInteractor
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
    }
}