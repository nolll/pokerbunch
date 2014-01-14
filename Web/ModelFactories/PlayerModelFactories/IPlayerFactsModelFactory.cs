using System.Collections.Generic;
using Core.Classes;
using Web.Models.PlayerModels.Facts;

namespace Web.ModelFactories.PlayerModelFactories
{
    public interface IPlayerFactsModelFactory
    {
        PlayerFactsModel Create(CurrencySettings currency, IList<Cashgame> cashgames, Player player);
    }
}