using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Web.Models.HomegameModels.Details;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameDetailsPageBuilder : IHomegameDetailsPageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAuth _auth;
        private readonly IBunchContextInteractor _contextInteractor;

        public HomegameDetailsPageBuilder(
            IHomegameRepository homegameRepository,
            IAuth auth,
            IBunchContextInteractor contextInteractor)
        {
            _homegameRepository = homegameRepository;
            _auth = auth;
            _contextInteractor = contextInteractor;
        }

        public HomegameDetailsPageModel Build(string slug)
        {
            var bunchContextRequest = new BunchContextRequest(slug);
            var contextResult = _contextInteractor.Execute(bunchContextRequest);
            var homegame = _homegameRepository.GetBySlug(slug);
            var isInManagerMode = _auth.IsInRole(slug, Role.Manager);
            return Create(contextResult, homegame, isInManagerMode);
        }

        private HomegameDetailsPageModel Create(BunchContextResult bunchContextResult, Homegame homegame, bool isInManagerMode)
        {
            var houseRules = FormatHouseRules(homegame.HouseRules);

            return new HomegameDetailsPageModel(bunchContextResult)
                {
	                DisplayName = homegame.DisplayName,
			        Description = homegame.Description,
			        HouseRules = houseRules,
	                ShowHouseRules = !string.IsNullOrEmpty(houseRules),
			        EditUrl = new EditHomegameUrl(homegame.Slug),
			        ShowEditLink = isInManagerMode
                };
        }

        private string FormatHouseRules(string houseRules)
        {
            return houseRules != null ? houseRules.Trim().Replace("\n\r", "<br />\n\r") : null;
        }
    }
}