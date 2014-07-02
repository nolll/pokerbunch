using Web.Models.CashgameModels.End;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public interface IEndPageBuilder
    {
        EndGamePageModel Build(string slug);
    }
}