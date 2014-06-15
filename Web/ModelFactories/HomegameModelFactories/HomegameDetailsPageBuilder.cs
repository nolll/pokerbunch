using Application.Services;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Web.Models.HomegameModels.Details;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameDetailsPageBuilder : IHomegameDetailsPageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAuth _auth;
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public HomegameDetailsPageBuilder(
            IHomegameRepository homegameRepository,
            IAuth auth,
            IBunchContextInteractor bunchContextInteractor)
        {
            _homegameRepository = homegameRepository;
            _auth = auth;
            _bunchContextInteractor = bunchContextInteractor;
        }

        public HomegameDetailsPageModel Build(string slug)
        {
            var bunchContextRequest = new BunchContextRequest {Slug = slug};
            var bunchContextResult = _bunchContextInteractor.Execute(bunchContextRequest);
            var homegame = _homegameRepository.GetBySlug(slug);
            var isInManagerMode = _auth.IsInRole(slug, Role.Manager);
            return Create(bunchContextResult, homegame, isInManagerMode);
        }

        public HomegameDetailsPageModel Create(BunchContextResult bunchContextResult, Homegame homegame, bool isInManagerMode)
        {
            var houseRules = FormatHouseRules(homegame.HouseRules);

            return new HomegameDetailsPageModel
                {
                    BrowserTitle = "Homegame Details",
                    PageProperties = new PageProperties(bunchContextResult),
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