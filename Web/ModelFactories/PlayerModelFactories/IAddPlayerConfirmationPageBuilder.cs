using Core.Entities;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IAddPlayerConfirmationPageBuilder
    {
        AddPlayerConfirmationPageModel Build(Homegame homegame);
    }
}