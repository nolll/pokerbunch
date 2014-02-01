using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Facts;

namespace Web.ModelFactories.CashgameModelFactories.Facts
{
    public interface ICashgameFactsPageModelFactory
    {
        CashgameFactsPageModel Create(User user, Homegame homegame, CashgameFacts facts, IList<int> years, int? year = null);
    }
}