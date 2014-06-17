using Web.Models.CashgameModels.End;

namespace Web.ModelFactories.CashgameModelFactories.End
{
    public interface IEndPageBuilder
    {
        EndPageModel Build(string slug);
    }
}