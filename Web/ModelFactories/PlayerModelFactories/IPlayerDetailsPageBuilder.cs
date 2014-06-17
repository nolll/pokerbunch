using Web.Models.PlayerModels.Details;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerDetailsPageBuilder
    {
        PlayerDetailsPageModel Build(string slug, int playerId);
    }
}