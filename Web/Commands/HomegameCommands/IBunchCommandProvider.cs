using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;

namespace Web.Commands.HomegameCommands
{
    public interface IBunchCommandProvider
    {
        Command GetAddCommand(AddBunchPostModel postModel);
        Command GetEditCommand(string slug, EditBunchPostModel postModel);
        Command GetJoinCommand(string slug, JoinBunchPostModel postModel);
    }
}