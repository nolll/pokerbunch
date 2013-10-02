using Core.Classes;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public interface ISharingIndexPageModelFactory
    {
        SharingIndexPageModel Create(User user, bool isSharing);
    }
}