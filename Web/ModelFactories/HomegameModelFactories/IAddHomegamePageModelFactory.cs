using Core.Classes;
using Web.Models.HomegameModels.Add;

namespace Web.Controllers
{
    public interface IAddHomegamePageModelFactory
    {
        AddHomegamePageModel Create(User user, Homegame homegame = null);
        AddHomegamePageModel ReBuild(User user, AddHomegamePageModel model);
    }
}