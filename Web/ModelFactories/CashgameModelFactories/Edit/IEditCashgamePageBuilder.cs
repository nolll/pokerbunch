using Web.Models.CashgameModels.Edit;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public interface IEditCashgamePageBuilder
    {
        EditCashgamePageModel Build(string slug, string dateStr, CashgameEditPostModel postModel = null);
    }
}