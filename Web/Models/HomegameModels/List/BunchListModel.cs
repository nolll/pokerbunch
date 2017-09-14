using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Extensions;

namespace Web.Models.HomegameModels.List
{
    public class BunchListModel : IViewModel
    {
        public List<BunchListItemModel> BunchModels { get; private set; }

        public BunchListModel(BunchList.Result bunchListResult)
        {
            BunchModels = bunchListResult.Bunches.Select(o => new BunchListItemModel(o)).ToList();
        }

        public View GetView()
        {
            return new View("BunchList/UserBunchList");
        }
    }
}