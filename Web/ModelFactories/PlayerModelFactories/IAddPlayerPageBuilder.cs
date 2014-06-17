using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IAddPlayerPageBuilder
    {
        AddPlayerPageModel Build(string slug, AddPlayerPostModel postModel = null);
    }
}