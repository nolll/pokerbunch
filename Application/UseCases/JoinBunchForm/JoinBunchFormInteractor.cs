using Core.Repositories;

namespace Application.UseCases.JoinBunchForm
{
    public static class JoinBunchFormInteractor
    {
        public static JoinBunchFormResult Execute(IBunchRepository bunchRepository, JoinBunchFormRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);

            return new JoinBunchFormResult(bunch.DisplayName);
        }
    }
}