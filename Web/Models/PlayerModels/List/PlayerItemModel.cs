using Core.UseCases;
using Web.Urls;

namespace Web.Models.PlayerModels.List
{
    public class PlayerItemModel
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public PlayerItemModel(PlayerList.PlayerListItem p)
        {
            Name = p.Name;
            Url = new PlayerDetailsUrl(p.Id).Relative;
        }
    }
}