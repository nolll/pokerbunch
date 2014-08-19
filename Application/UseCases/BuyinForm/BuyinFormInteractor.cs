using Core.Repositories;

namespace Application.UseCases.BuyinForm
{
    public class BuyinFormInteractor
    {
        private readonly IHomegameRepository _homegameRepository;

        public BuyinFormInteractor(
            IHomegameRepository homegameRepository)
        {
            _homegameRepository = homegameRepository;
        }

        public BuyinFormResult Execute(BuyinFormRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);

            return new BuyinFormResult(homegame.DefaultBuyin);
        }
    }
}