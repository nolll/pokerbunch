using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Listing;

namespace Web.ModelFactories.CashgameModelFactories.Listing
{
    public interface ICashgameListingTableModelFactory
    {
        CashgameListingTableModel Create(Homegame homegame, IList<Cashgame> cashgames);
    }
}