using Core.Repositories;

namespace Application.UseCases.BunchList
{
    public class BunchListInteractor : IBunchListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;

        public BunchListInteractor(
            IHomegameRepository homegameRepository)
        {
            _homegameRepository = homegameRepository;
        }

        public BunchListResult Execute()
        {
            var homegames = _homegameRepository.GetList();
            
            return new BunchListResult(homegames);
        }
    }
}