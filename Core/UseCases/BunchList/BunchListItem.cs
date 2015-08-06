using Core.Entities;
using Core.Urls;

namespace Core.UseCases.BunchList
{
    public class BunchListItem
    {
        public string Slug { get; private set; }
        public string DisplayName { get; private set; }
        public Url Url { get; private set; }

        public BunchListItem(Bunch bunch)
        {
            Slug = bunch.Slug;
            DisplayName = bunch.DisplayName;
            Url = new BunchDetailsUrl(bunch.Slug);
        }
    }
}