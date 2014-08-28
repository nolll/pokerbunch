using Application.Urls;
using Application.UseCases.AppContext;
using Core.Repositories;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinBunchConfirmationPageBuilder : IJoinBunchConfirmationPageBuilder
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IAppContextInteractor _contextInteractor;

        public JoinBunchConfirmationPageBuilder(
            IBunchRepository bunchRepository,
            IAppContextInteractor contextInteractor)
        {
            _bunchRepository = bunchRepository;
            _contextInteractor = contextInteractor;
        }

        public JoinBunchConfirmationPageModel Build(string slug)
        {
            var bunch = _bunchRepository.GetBySlug(slug);

            var contextResult = _contextInteractor.Execute();

            return new JoinBunchConfirmationPageModel(contextResult)
                {
                    BunchUrl = new BunchDetailsUrl(bunch.Slug),
                    BunchName = bunch.DisplayName
                };
        }
    }
}