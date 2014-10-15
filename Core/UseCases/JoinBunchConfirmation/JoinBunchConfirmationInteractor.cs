using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.JoinBunchConfirmation
{
    public static class JoinBunchConfirmationInteractor
    {
        public static JoinBunchConfirmationResult Execute(IBunchRepository bunchRepository, JoinBunchConfirmationRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var bunchName = bunch.DisplayName;

            var detailsUrl = new BunchDetailsUrl(request.Slug);
            
            return new JoinBunchConfirmationResult(bunchName, detailsUrl);
        }
    }
}