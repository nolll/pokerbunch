using System.Collections.Generic;
using Core.Classes;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameListPageModelFactory
    {
        HomegameListPageModel Create(User user, IEnumerable<Homegame> homegames);
    }
}