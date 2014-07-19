using Application.Urls;
using Application.UseCases.PlayerList;

namespace Web.Models.PlayerModels.List
{
    public class PlayerItemModel
    {
        public string Name { get; private set; }
        public Url Url { get; private set; }

        public PlayerItemModel(string slug, PlayerListItem p)
        {
            Name = p.Name;
            Url = new PlayerDetailsUrl(slug, p.Id);
        }
    }
}