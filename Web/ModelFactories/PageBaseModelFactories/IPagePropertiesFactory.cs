using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public interface IPagePropertiesFactory
    {
        PageProperties Create();
        PageProperties Create(Homegame homegame);
    }
}