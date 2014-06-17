using System.Collections.Generic;
using Core.Entities;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public interface ICashgameEditPageBuilder
    {
        CashgameEditPageModel Build(Homegame homegame, Cashgame cashgame, IEnumerable<string> locations, CashgameEditPostModel postModel);
    }
}