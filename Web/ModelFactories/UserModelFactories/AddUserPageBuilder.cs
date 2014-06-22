using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public class AddUserPageBuilder : IAddUserPageBuilder
    {
        private readonly IAppContextInteractor _contextInteractor;

        public AddUserPageBuilder(IAppContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public AddUserPageModel Build(AddUserPostModel postModel)
        {
            var contextResult = _contextInteractor.Execute();

            var model = new AddUserPageModel(contextResult);

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