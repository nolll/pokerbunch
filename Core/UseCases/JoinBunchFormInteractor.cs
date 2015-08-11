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

        public class JoinBunchFormRequest
        {
            public string Slug { get; private set; }

            public JoinBunchFormRequest(string slug)
            {
                Slug = slug;
            }
        }

        public class JoinBunchFormResult
        {
            public string BunchName { get; private set; }

            public JoinBunchFormResult(string bunchName)
            {
                BunchName = bunchName;
            }
        }
    }
}