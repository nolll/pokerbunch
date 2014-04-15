using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistPageBuilder
    {
        CashgameToplistPageModel Build(string slug, string sortOrderParam, int? year);
    }
}