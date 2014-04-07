using System.Linq;
using Core.Classes;
using Core.Repositories;

namespace Core.UseCases.ShowBunchList
{
    public class ShowBunchListInteractor : IShowBunchListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;

        public ShowBunchListInteractor(
            IHomegameRepository homegameRepository)
        {
            _homegameRepository = homegameRepository;
        }

        public ShowBunchListResult Execute()
        {
            var homegames = _homegameRepository.GetList();
            var itemList = homegames.Select(CreateItem).ToList();
            return new ShowBunchListResult
                {
                    Bunches = itemList
                };
        }

        private BunchListItem CreateItem(Homegame homegame)
        {
            return new BunchListItem
                {
                    DisplayName = homegame.DisplayName,
                    Slug = homegame.Slug
                };
        }
    }
}