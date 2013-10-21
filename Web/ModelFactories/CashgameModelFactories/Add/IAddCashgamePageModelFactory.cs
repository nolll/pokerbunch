using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Add;

namespace Web.ModelFactories.CashgameModelFactories.Add
{
    public interface IAddCashgamePageModelFactory
    {
        AddCashgamePageModel Create(User user, Homegame homegame, IEnumerable<string> locations);
        AddCashgamePageModel Create(User user, Homegame homegame, IEnumerable<string> locations, AddCashgamePostModel postModel);
    }
}