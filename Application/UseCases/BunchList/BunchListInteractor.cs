using Core.Repositories;

namespace Application.UseCases.BunchList
{
    public class BunchListInteractor : IBunchListInteractor
    {
        private readonly IBunchRepository _bunchRepository;

        public BunchListInteractor(
            IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public BunchListResult Execute()
        {
            var homegames = _bunchRepository.GetList();
            
            return new BunchListResult(homegames);
        }
    }
}