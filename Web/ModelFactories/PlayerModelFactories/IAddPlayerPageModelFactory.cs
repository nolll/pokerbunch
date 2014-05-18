using Core.Entities;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IAddPlayerPageModelFactory
    {
        AddPlayerPageModel Create(Homegame homegame, AddPlayerPostModel postModel = null);
    }
}