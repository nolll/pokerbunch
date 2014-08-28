using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Web.Models.HomegameModels.Details;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchDetailsPageBuilder : IBunchDetailsPageBuilder
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IAuth _auth;
        private readonly IBunchContextInteractor _contextInteractor;

        public BunchDetailsPageBuilder(
            IBunchRepository bunchRepository,
            IAuth auth,
            IBunchContextInteractor contextInteractor)
        {
            _bunchRepository = bunchRepository;
            _auth = auth;
            _contextInteractor = contextInteractor;
        }

        public BunchDetailsPageModel Build(string slug)
        {
            var bunchContextRequest = new BunchContextRequest(slug);
            var contextResult = _contextInteractor.Execute(bunchContextRequest);
            var bunch = _bunchRepository.GetBySlug(slug);
            var isInManagerMode = _auth.IsInRole(slug, Role.Manager);
            return Create(contextResult, bunch, isInManagerMode);
        }

        private BunchDetailsPageModel Create(BunchContextResult bunchContextResult, Bunch bunch, bool isInManagerMode)
        {
            var houseRules = FormatHouseRules(bunch.HouseRules);

            return new BunchDetailsPageModel(bunchContextResult)
                {
	                DisplayName = bunch.DisplayName,
			        Description = bunch.Description,
			        HouseRules = houseRules,
	                ShowHouseRules = !string.IsNullOrEmpty(houseRules),
			        EditUrl = new EditBunchUrl(bunch.Slug),
			        ShowEditLink = isInManagerMode
                };
        }

        private string FormatHouseRules(string houseRules)
        {
            return houseRules != null ? houseRules.Trim().Replace("\n\r", "<br />\n\r") : null;
        }
    }
}