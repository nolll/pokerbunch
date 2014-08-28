using Core.Repositories;

namespace Application.UseCases.JoinBunchForm
{
    public class JoinBunchFormInteractor : IJoinBunchFormInteractor
    {
        private readonly IHomegameRepository _homegameRepository;

        public JoinBunchFormInteractor(IHomegameRepository homegameRepository)
        {
            _homegameRepository = homegameRepository;
        }

        public JoinBunchFormResult Execute(JoinBunchFormRequest request)
        {
            var bunch = _homegameRepository.GetBySlug(request.Slug);

            return new JoinBunchFormResult(bunch.DisplayName);
        }
    }
}