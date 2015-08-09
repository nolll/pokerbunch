using System.Collections.Generic;
using System.Linq;
using Core.UseCases;

namespace Web.Models.HomegameModels.List
{
    public class BunchListModel
    {
        public List<BunchListItemModel> BunchModels { get; private set; }

        public BunchListModel(BunchList.Result bunchListResult)
        {
            BunchModels = bunchListResult.Bunches.Select(o => new BunchListItemModel(o)).ToList();
        }
    }
}