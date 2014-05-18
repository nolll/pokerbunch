using Core.Entities;
using Web.Models.CashgameModels.End;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public interface IEndPageModelFactory
    {
        EndPageModel Create(Homegame homegame);
    }
}