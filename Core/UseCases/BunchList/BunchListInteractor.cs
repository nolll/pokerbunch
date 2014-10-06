using Core.Repositories;

namespace Core.UseCases.BunchList
{
    public static class BunchListInteractor
    {
        public static BunchListResult Execute(IBunchRepository bunchRepository)
        {
            var homegames = bunchRepository.GetList();
            
            return new BunchListResult(homegames);
        }
    }
}