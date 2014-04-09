using Web.Models.PlayerModels.List;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerListPageBuilder
    {
        PlayerListPageModel Build(string slug);
    }
}