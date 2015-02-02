using Core.Repositories;

namespace Core.UseCases.JoinBunchForm
{
    public class JoinBunchFormInteractor
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