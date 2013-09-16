using Core.Classes;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface IEndPageModelFactory
    {
        EndPageModel Create(User user, Homegame homegame, Cashgame runningGame);
    }
}