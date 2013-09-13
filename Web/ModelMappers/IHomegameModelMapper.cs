using Core.Classes;
using Web.Models.HomegameModels.Add;

namespace Web.ModelMappers
{
    public interface IHomegameModelMapper
    {
        Homegame GetHomegame(AddHomegamePageModel model);
    }
}