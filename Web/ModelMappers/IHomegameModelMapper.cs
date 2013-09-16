using Core.Classes;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;

namespace Web.ModelMappers
{
    public interface IHomegameModelMapper
    {
        Homegame GetHomegame(AddHomegamePostModel postModel);
        Homegame GetHomegame(Homegame homegame, HomegameEditPostModel postModel);
    }
}