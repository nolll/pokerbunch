using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.Edit;

namespace Web.ModelFactories.UserModelFactories
{
    public class EditUserPageBuilder : IEditUserPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUserRepository _userRepository;

        public EditUserPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IUserRepository userRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _userRepository = userRepository;
        }

        public EditUserPageModel Build(string userName, EditUserPostModel postModel)
        {
            var user = _userRepository.GetByNameOrEmail(userName);

            var model = Build(user);
            if (postModel != null)
            {
                model.RealName = postModel.RealName;
                model.DisplayName = postModel.DisplayName;
                model.Email = postModel.Email;
            }
            return model;
        }

        private EditUserPageModel Build(User user)
        {
            return new EditUserPageModel
                {
                    BrowserTitle = "Edit Profile",
                    PageProperties = _pagePropertiesFactory.Create(),
                    UserName = user.UserName,
                    RealName = user.RealName,
                    DisplayName = user.DisplayName,
                    Email = user.Email
                };
        }
    }
}