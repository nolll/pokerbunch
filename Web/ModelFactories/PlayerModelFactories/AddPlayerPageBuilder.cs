using Application.UseCases.BunchContext;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerPageBuilder : IAddPlayerPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;

        public AddPlayerPageBuilder(
            IBunchContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public AddPlayerPageModel Build(string slug, AddPlayerPostModel postModel = null)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            var model = new AddPlayerPageModel(contextResult);

            if (postModel != null)
            {
                model.Name = postModel.Name;
            }

            return model;
        }
    }
}