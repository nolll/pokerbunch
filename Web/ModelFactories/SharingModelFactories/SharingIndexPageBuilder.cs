using Application.Services;
using Application.Urls;
using Application.UseCases.ApplicationContext;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PageBaseModels;
using Web.Models.SharingModels;

namespace Web.ModelFactories.SharingModelFactories
{
    public class SharingIndexPageBuilder : ISharingIndexPageBuilder
    {
        private readonly IAuth _auth;
        private readonly ISharingRepository _sharingRepository;
        private readonly IAppContextInteractor _appContextInteractor;

        public SharingIndexPageBuilder(
            IAuth auth,
            ISharingRepository sharingRepository,
            IAppContextInteractor appContextInteractor)
        {
            _auth = auth;
            _sharingRepository = sharingRepository;
            _appContextInteractor = appContextInteractor;
        }

        public SharingIndexPageModel Build()
        {
            var user = _auth.CurrentUser;
            var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);

            var applicationContextResult = _appContextInteractor.Execute();
            
            return new SharingIndexPageModel
                {
                    BrowserTitle = "Sharing",
                    PageProperties = new PageProperties(applicationContextResult),
                    IsSharingToTwitter = isSharing,
			        ShareToTwitterSettingsUrl = new TwitterSettingsUrl()
                };
        }
    }
}