using Core.Classes;
using Web.Models.HomegameModels.Add;

namespace Web.Controllers
{
    public interface IHomegameModelMapper
    {
        Homegame GetHomegame(AddHomegamePageModel model);
    }
}