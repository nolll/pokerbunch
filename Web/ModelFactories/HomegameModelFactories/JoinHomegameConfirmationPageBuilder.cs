using Application.Urls;
using Application.UseCases.AppContext;
using Core.Repositories;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegameConfirmationPageBuilder : IJoinHomegameConfirmationPageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAppContextInteractor _contextInteractor;

        public JoinHomegameConfirmationPageBuilder(
            IHomegameRepository homegameRepository,
            IAppContextInteractor contextInteractor)
        {
            _homegameRepository = homegameRepository;
            _contextInteractor = contextInteractor;
        }

        public JoinHomegameConfirmationPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            var contextResult = _contextInteractor.Execute();

            return new JoinHomegameConfirmationPageModel(contextResult)
                {
                    BunchUrl = new HomegameDetailsUrl(homegame.Slug),
                    BunchName = homegame.DisplayName
                };
        }
    }
}