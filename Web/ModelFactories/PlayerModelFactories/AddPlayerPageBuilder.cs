using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerPageBuilder : IAddPlayerPageBuilder
    {
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public AddPlayerPageBuilder(
            IBunchContextInteractor bunchContextInteractor)
        {
            _bunchContextInteractor = bunchContextInteractor;
        }

        public AddPlayerPageModel Build(string slug, AddPlayerPostModel postModel = null)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest{Slug = slug});

            var model = new AddPlayerPageModel
                {
                    BrowserTitle = "Add Player",
                    PageProperties = new PageProperties(contextResult)
                };
            if (postModel != null)
            {
                model.Name = postModel.Name;
            }
            return model;
        }
    }
}