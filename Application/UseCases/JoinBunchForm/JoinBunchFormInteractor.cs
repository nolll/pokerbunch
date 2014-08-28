using Core.Repositories;

namespace Application.UseCases.JoinBunchForm
{
    public class JoinBunchFormInteractor : IJoinBunchFormInteractor
    {
        private readonly IBunchRepository _bunchRepository;

        public JoinBunchFormInteractor(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public JoinBunchFormResult Execute(JoinBunchFormRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);

            return new JoinBunchFormResult(bunch.DisplayName);
        }
    }
}