using Web.Models.AuthModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public interface IAuthLoginPageModelFactory
    {
        AuthLoginPageModel Create(AuthLoginPostModel postModel);
    }
}