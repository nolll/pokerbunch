using Application.Services;
using Application.Urls;
using Application.UseCases.AppContext;
using Core.Entities;
using Core.Repositories;
using Web.Models.PageBaseModels;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingIndexPageBuilder : ISharingIndexPageBuilder
    {
        private readonly IAuth _auth;
        private readonly ISharingRepository _sharingRepository;
        private readonly IAppContextInteractor _contextInteractor;

        public SharingIndexPageBuilder(
            IAuth auth,
            ISharingRepository sharingRepository,
            IAppContextInteractor contextInteractor)
        {
            _auth = auth;
            _sharingRepository = sharingRepository;
            _contextInteractor = contextInteractor;
        }

        public SharingIndexPageModel Build()
        {
            var user = _auth.CurrentUser;
            var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);

            var contextResult = _contextInteractor.Execute();
            
            return new SharingIndexPageModel
                {
                    BrowserTitle = "Sharing",
                    PageProperties = new PageProperties(contextResult),
                    IsSharingToTwitter = isSharing,
			        ShareToTwitterSettingsUrl = new TwitterSettingsUrl()
                };
        }
    }
}