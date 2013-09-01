using Web.Models.AuthModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public interface IAuthLoginPageModelFactory
    {
        AuthLoginPageModel Create(string returnUrl, string loginName);
    }
}