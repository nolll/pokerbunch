using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public interface ISharingIndexPageModelFactory
    {
        SharingIndexPageModel Create(bool isSharing);
    }
}