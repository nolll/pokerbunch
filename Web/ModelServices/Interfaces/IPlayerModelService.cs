using Core.Classes;
using Web.Models.PlayerModels.Details;

namespace Web.ModelServices
{
    public interface IPlayerModelService
    {
        PlayerDetailsPageModel GetDetailsModel(User currentUser, Homegame homegame, string playerName);
    }
}