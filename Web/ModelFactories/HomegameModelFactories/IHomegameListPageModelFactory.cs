using Core.UseCases;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameListPageModelFactory
    {
        HomegameListPageModel Create(ShowBunchListResult showBunchListResult);
    }
}