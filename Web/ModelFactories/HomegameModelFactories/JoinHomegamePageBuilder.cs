using Application.UseCases.AppContext;
using Core.Repositories;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegamePageBuilder : IJoinHomegamePageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAppContextInteractor _contextInteractor;

        public JoinHomegamePageBuilder(
            IHomegameRepository homegameRepository,
            IAppContextInteractor contextInteractor)
        {
            _homegameRepository = homegameRepository;
            _contextInteractor = contextInteractor;
        }

        public JoinHomegamePageModel Build(string slug, JoinHomegamePostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            var contextResult = _contextInteractor.Execute();

            var model = new JoinHomegamePageModel(contextResult)
            {
                Name = homegame.DisplayName
            };

            if (postModel != null)
            {
                model.Code = postModel.Code;
            }
            
            return model;
        }

    }
}