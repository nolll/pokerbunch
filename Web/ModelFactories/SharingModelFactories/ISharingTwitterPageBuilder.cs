using Core.Entities;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public interface ISharingTwitterPageBuilder
    {
        SharingTwitterPageModel Build(bool isSharing, TwitterCredentials credentials);
    }
}