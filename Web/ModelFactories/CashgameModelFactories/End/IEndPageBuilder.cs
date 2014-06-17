using Core.Entities;
using Web.Models.CashgameModels.End;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public interface IEndPageBuilder
    {
        EndPageModel Build(Homegame homegame);
    }
}