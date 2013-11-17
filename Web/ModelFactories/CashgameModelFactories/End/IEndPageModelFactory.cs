using Core.Classes;
using Web.Models.CashgameModels.End;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public interface IEndPageModelFactory
    {
        EndPageModel Create(User user, Homegame homegame);
    }
}