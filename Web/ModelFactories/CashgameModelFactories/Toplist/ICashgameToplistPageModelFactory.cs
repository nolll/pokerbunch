using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Toplist;

namespace Web.ModelFactories.CashgameModelFactories.Toplist
{
    public interface ICashgameToplistPageModelFactory
    {
        CashgameToplistPageModel Create(User user, Homegame homegame, CashgameSuite suite, IList<int> years, ToplistSortOrder sortOrder, int? year);
    }
}