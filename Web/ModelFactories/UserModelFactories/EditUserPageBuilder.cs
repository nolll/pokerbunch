using Application.UseCases.AppContext;
using Core.Repositories;
using Web.Models.UserModels.Edit;

namespace Web.ModelFactories.UserModelFactories
{
    public class EditUserPageBuilder : IEditUserPageBuilder
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppContextInteractor _contextInteractor;

        public EditUserPageBuilder(
            IUserRepository userRepository,
            IAppContextInteractor contextInteractor)
        {
            _userRepository = userRepository;
            _contextInteractor = contextInteractor;
        }

        public EditUserPageModel Build(string userName, EditUserPostModel postModel)
        {
            var user = _userRepository.GetByNameOrEmail(userName);

            var contextResult = _contextInteractor.Execute();

            var model = new EditUserPageModel(contextResult)
            {
                UserName = user.UserName,
                RealName = user.RealName,
                DisplayName = user.DisplayName,
                Email = user.Email
            };

            if (postModel != null)
            {
                model.RealName = postModel.RealName;
                model.DisplayName = postModel.DisplayName;
                model.Email = postModel.Email;
            }
            
            return model;
        }
    }
}