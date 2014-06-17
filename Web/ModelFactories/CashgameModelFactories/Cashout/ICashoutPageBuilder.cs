using Web.Models.CashgameModels.Cashout;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public interface ICashoutPageBuilder
    {
        CashoutPageModel Build(string slug, CashoutPostModel postModel = null);
    }
}