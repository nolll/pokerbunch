using Web.Models.AuthModels;

namespace Web.ModelServices
{
    public interface IAuthModelService
    {
        AuthLoginPageModel GetLoginModel(AuthLoginPostModel postModel = null);
    }
}