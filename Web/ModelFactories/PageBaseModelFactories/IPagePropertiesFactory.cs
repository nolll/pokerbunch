using Application.UseCases.CashgameContext;
using Core.Classes;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.PageBaseModelFactories
{
    public interface IPagePropertiesFactory
    {
        PageProperties Create(Homegame homegame = null);
        PageProperties Create(BunchContextResult bunchContextResult);
    }
}