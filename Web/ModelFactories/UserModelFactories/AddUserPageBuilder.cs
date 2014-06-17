using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public class AddUserPageBuilder : IAddUserPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddUserPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
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

        public AddUserPageModel Build(AddUserPostModel postModel)
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