using Core.Entities;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IAddPlayerPageBuilder
    {
        AddPlayerPageModel Build(Homegame homegame, AddPlayerPostModel postModel = null);
    }
}