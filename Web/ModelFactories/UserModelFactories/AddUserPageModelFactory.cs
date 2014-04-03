using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public class AddUserPageModelFactory : IAddUserPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddUserPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        private AddUserPageModel Create()
        {
            return new AddUserPageModel
                {
                    BrowserTitle = "Register",
                    PageProperties = _pagePropertiesFactory.Create()
                };
        }

        public AddUserPageModel Create(AddUserPostModel postModel)
        {
            var model = Create();
            if (postModel != null)
            {
                model.UserName = postModel.UserName;
                model.DisplayName = postModel.DisplayName;
                model.Email = postModel.Email;
            }
            return model;
        }

    }
}