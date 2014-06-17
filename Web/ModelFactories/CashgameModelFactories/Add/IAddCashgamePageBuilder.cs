using System.Collections.Generic;
using Core.Entities;
using Web.Models.CashgameModels.Add;

namespace Web.ModelFactories.CashgameModelFactories.Add
{
    public interface IAddCashgamePageBuilder
    {
        AddCashgamePageModel Build(Homegame homegame, IEnumerable<string> locations, AddCashgamePostModel postModel);
    }
}