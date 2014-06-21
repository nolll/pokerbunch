using Application.UseCases.BunchContext;
using Core.Repositories;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerConfirmationPageBuilder : IAddPlayerConfirmationPageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IBunchContextInteractor _contextInteractor;

        public AddPlayerConfirmationPageBuilder(
            IHomegameRepository homegameRepository,
            IBunchContextInteractor contextInteractor)
        {
            _homegameRepository = homegameRepository;
            _contextInteractor = contextInteractor;
        }

        public AddPlayerConfirmationPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));
            
            return new AddPlayerConfirmationPageModel(contextResult)
                {
                    HomegameName = homegame.DisplayName
                };
        }
    }
}