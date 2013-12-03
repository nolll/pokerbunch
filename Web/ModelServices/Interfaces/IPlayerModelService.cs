using Core.Classes;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Details;
using Web.Models.PlayerModels.Invite;
using Web.Models.PlayerModels.List;

namespace Web.ModelServices
{
    public interface IPlayerModelService
    {
        PlayerListPageModel GetListModel(Homegame homegame);
        PlayerDetailsPageModel GetDetailsModel(Homegame homegame, string playerName);
        AddPlayerPageModel GetAddModel(Homegame homegame, AddPlayerPostModel postModel = null);
        AddPlayerConfirmationPageModel GetAddConfirmationModel(Homegame homegame);
        InvitePlayerPageModel GetInviteModel(Homegame homegame);
        InvitePlayerConfirmationPageModel GetInviteConfirmationModel(Homegame homegame);
    }
}