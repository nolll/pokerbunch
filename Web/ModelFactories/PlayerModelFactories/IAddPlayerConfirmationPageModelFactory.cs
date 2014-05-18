using Core.Entities;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IAddPlayerConfirmationPageModelFactory
    {
        AddPlayerConfirmationPageModel Create(Homegame homegame);
    }
}