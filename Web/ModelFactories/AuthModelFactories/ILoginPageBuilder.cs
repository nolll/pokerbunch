using Web.Models.AuthModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public interface ILoginPageBuilder
    {
        LoginPageModel Build(LoginPostModel postModel = null);
    }
}