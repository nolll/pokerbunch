using Core.Entities;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public interface IPagePropertiesFactory
    {
        PageProperties Create(Homegame homegame = null);
    }
}