using Core.Entities;

namespace Application.UseCases.BunchList
{
    public class BunchListItem
    {
        public string DisplayName { get; private set; }
        public string Slug { get; private set; }

        public BunchListItem(Homegame homegame)
        {
            DisplayName = homegame.DisplayName;
            Slug = homegame.Slug;
        }
    }
}