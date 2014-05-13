using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface IToplistPageBuilder
    {
        CashgameToplistPageModel Build(string slug, string sortOrderParam, int? year);
    }
}