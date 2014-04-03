using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Add;

namespace Web.ModelFactories.CashgameModelFactories.Add
{
    public interface IAddCashgamePageModelFactory
    {
        AddCashgamePageModel Create(Homegame homegame, IEnumerable<string> locations, AddCashgamePostModel postModel);
    }
}