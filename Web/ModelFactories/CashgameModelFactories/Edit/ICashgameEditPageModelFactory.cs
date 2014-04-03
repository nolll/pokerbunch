﻿using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelFactories.CashgameModelFactories.Edit
{
    public interface ICashgameEditPageModelFactory
    {
        CashgameEditPageModel Create(Homegame homegame, Cashgame cashgame, IEnumerable<string> locations, CashgameEditPostModel postModel);
    }
}