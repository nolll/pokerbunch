using Core.Classes;
using Web.Models.CashgameModels.Listing;

namespace Web.ModelFactories.CashgameModelFactories.Listing
{
    public interface ICashgameListingTableItemModelFactory
    {
        CashgameListingTableItemModel Create(Homegame homegame, Cashgame cashgame, bool showYear);
    }
}