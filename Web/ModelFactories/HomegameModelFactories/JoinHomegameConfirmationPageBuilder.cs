using Application.Urls;
using Application.UseCases.AppContext;
using Core.Repositories;
using Web.Models.HomegameModels.Join;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegameConfirmationPageBuilder : IJoinHomegameConfirmationPageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAppContextInteractor _appContextInteractor;

        public JoinHomegameConfirmationPageBuilder(
            IHomegameRepository homegameRepository,
            IAppContextInteractor appContextInteractor)
        {
            _homegameRepository = homegameRepository;
            _appContextInteractor = appContextInteractor;
        }

        public JoinHomegameConfirmationPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            var contextResult = _appContextInteractor.Execute();

            return new JoinHomegameConfirmationPageModel
                {
                    BrowserTitle = "Welcome",
                    PageProperties = new PageProperties(contextResult),
                    BunchUrl = new HomegameDetailsUrl(homegame.Slug),
                    BunchName = homegame.DisplayName
                };
        }
    }
}