using Application.UseCases.AppContext;
using Core.Repositories;
using Web.Models.HomegameModels.Join;
using Web.Models.PageBaseModels;

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

        private JoinHomegamePageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            var contextResult = _contextInteractor.Execute();

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