using Web.Models.SharingModels;

namespace Web.ModelServices
{
    public interface ISharingModelService
    {
        SharingIndexPageModel GetIndexModel();
        SharingTwitterPageModel GetTwitterModel();
    }
}