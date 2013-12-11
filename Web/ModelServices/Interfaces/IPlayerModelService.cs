﻿using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Details;
using Web.Models.PlayerModels.Invite;
using Web.Models.PlayerModels.List;

namespace Web.ModelServices
{
    public interface IPlayerModelService
    {
        PlayerListPageModel GetListModel(string slug);
        PlayerDetailsPageModel GetDetailsModel(string slug, string playerName);
        AddPlayerPageModel GetAddModel(string slug, AddPlayerPostModel postModel = null);
        AddPlayerConfirmationPageModel GetAddConfirmationModel(string slug);
        InvitePlayerPageModel GetInviteModel(string slug);
        InvitePlayerConfirmationPageModel GetInviteConfirmationModel(string slug);
    }
}