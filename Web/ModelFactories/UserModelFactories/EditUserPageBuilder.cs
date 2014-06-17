using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.Edit;

namespace Web.ModelFactories.UserModelFactories
{
    public class EditUserPageBuilder : IEditUserPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public EditUserPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        private EditUserPageModel Create(User user)
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

        public EditUserPageModel Build(User user, EditUserPostModel postModel)
        {
            var model = Create(user);
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