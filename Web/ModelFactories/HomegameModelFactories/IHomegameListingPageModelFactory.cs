using System.Collections.Generic;
using Core.Classes;
using Web.Models.HomegameModels.Listing;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameListingPageModelFactory
    {
        HomegameListingPageModel Create(User user, IEnumerable<Homegame> homegames);
    }
}