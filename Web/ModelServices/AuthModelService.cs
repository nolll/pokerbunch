using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;

namespace Web.ModelServices
{
    public class AuthModelService : IAuthModelService
    {
        private readonly IAuthLoginPageModelFactory _authLoginPageModelFactory;

        public AuthModelService(IAuthLoginPageModelFactory authLoginPageModelFactory)
        {
            _authLoginPageModelFactory = authLoginPageModelFactory;
        }

        public AuthLoginPageModel GetLoginModel(AuthLoginPostModel postModel)
        {
            return _authLoginPageModelFactory.Create(postModel);
        }
    }
}