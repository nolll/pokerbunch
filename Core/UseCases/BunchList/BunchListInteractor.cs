using Core.Repositories;

namespace Core.UseCases.BunchList
{
    public class BunchListInteractor
    {
        private readonly IBunchRepository _bunchRepository;

        public BunchListInteractor(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public BunchListResult Execute()
        {
            var homegames = _bunchRepository.GetList();

            return new BunchListResult(homegames);
        }

        public BunchListResult Execute(BunchListRequest request)
        {
            var homegames = _bunchRepository.GetByUserId(request.UserId);
            
            return new BunchListResult(homegames);
        }
    }
}