using Application.UseCases.AppContext;
using Core.Repositories;
using Web.Models.HomegameModels.Join;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegamePageBuilder : IJoinHomegamePageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAppContextInteractor _appContextInteractor;

        public JoinHomegamePageBuilder(
            IHomegameRepository homegameRepository,
            IAppContextInteractor appContextInteractor)
        {
            _homegameRepository = homegameRepository;
            _appContextInteractor = appContextInteractor;
        }

        private JoinHomegamePageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            var contextResult = _appContextInteractor.Execute();

            return new JoinHomegamePageModel
                {
                    BrowserTitle = "Join Bunch",
                    PageProperties = new PageProperties(contextResult),
                    Name = homegame.DisplayName
                };
        }

        public JoinHomegamePageModel Build(string slug, JoinHomegamePostModel postModel)
        {
            var model = Build(slug);
            if (postModel != null)
            {
                model.Code = postModel.Code;
            }
            return model;
        }

    }
}