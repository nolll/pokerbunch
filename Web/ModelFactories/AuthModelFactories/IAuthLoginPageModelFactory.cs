using Web.Models.AuthModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public interface IAuthLoginPageModelFactory
    {
        AuthLoginPageModel Create();
        AuthLoginPageModel Create(AuthLoginPostModel postModel);
    }
}