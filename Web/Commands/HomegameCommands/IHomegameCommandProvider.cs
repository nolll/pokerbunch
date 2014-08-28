using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;

namespace Web.Commands.HomegameCommands
{
    public interface IHomegameCommandProvider
    {
        Command GetAddCommand(AddBunchPostModel postModel);
        Command GetEditCommand(string slug, HomegameEditPostModel postModel);
        Command GetJoinCommand(string slug, JoinHomegamePostModel postModel);
    }
}