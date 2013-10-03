using Core.Classes;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public interface ISharingTwitterPageModelFactory
    {
        SharingTwitterPageModel Create(User user, bool isSharing, TwitterCredentials credentials);
    }
}