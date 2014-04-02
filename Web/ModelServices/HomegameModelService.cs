using Application.Services;
using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Details;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Models.HomegameModels.List;
using Web.Security;

namespace Web.ModelServices
{
    public class HomegameModelService : IHomegameModelService
    {
        private readonly IAuthentication _authentication;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IHomegameListPageModelFactory _homegameListPageModelFactory;
        private readonly IHomegameDetailsPageModelFactory _homegameDetailsPageModelFactory;
        private readonly IAddHomegamePageModelFactory _addHomegamePageModelFactory;
        private readonly IAddHomegameConfirmationPageModelFactory _addHomegameConfirmationPageModelFactory;
        private readonly IHomegameEditPageModelFactory _homegameEditPageModelFactory;
        private readonly IJoinHomegamePageModelFactory _joinHomegamePageModelFactory;
        private readonly IJoinHomegameConfirmationPageModelFactory _joinHomegameConfirmationPageModelFactory;
        private readonly IAuthenticationService _authenticationService;

        public HomegameModelService(
            IAuthentication authentication,
            IHomegameRepository homegameRepository,
            IHomegameListPageModelFactory homegameListPageModelFactory,
            IHomegameDetailsPageModelFactory homegameDetailsPageModelFactory,
            IAddHomegamePageModelFactory addHomegamePageModelFactory,
            IAddHomegameConfirmationPageModelFactory addHomegameConfirmationPageModelFactory,
            IHomegameEditPageModelFactory homegameEditPageModelFactory,
            IJoinHomegamePageModelFactory joinHomegamePageModelFactory,
            IJoinHomegameConfirmationPageModelFactory joinHomegameConfirmationPageModelFactory,
            IAuthenticationService authenticationService)
        {
            _authentication = authentication;
            _homegameRepository = homegameRepository;
            _homegameListPageModelFactory = homegameListPageModelFactory;
            _homegameDetailsPageModelFactory = homegameDetailsPageModelFactory;
            _addHomegamePageModelFactory = addHomegamePageModelFactory;
            _addHomegameConfirmationPageModelFactory = addHomegameConfirmationPageModelFactory;
            _homegameEditPageModelFactory = homegameEditPageModelFactory;
            _joinHomegamePageModelFactory = joinHomegamePageModelFactory;
            _joinHomegameConfirmationPageModelFactory = joinHomegameConfirmationPageModelFactory;
            _authenticationService = authenticationService;
        }

        public HomegameListPageModel GetListModel()
        {
            var user = _authentication.GetUser();
            var homegames = _homegameRepository.GetList();
            return _homegameListPageModelFactory.Create(user, homegames);
        }

        public HomegameDetailsPageModel GetDetailsModel(string slug)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var isInManagerMode = _authenticationService.IsInRole(slug, Role.Manager);
            return _homegameDetailsPageModelFactory.Create(user, homegame, isInManagerMode);
        }

        public AddHomegamePageModel GetAddModel(AddHomegamePostModel postModel)
        {
            var user = _authentication.GetUser();
            return _addHomegamePageModelFactory.Create(user, postModel);
        }

        public AddHomegameConfirmationPageModel GetAddConfirmationModel()
        {
            var user = _authentication.GetUser();
            return _addHomegameConfirmationPageModelFactory.Create(user);
        }

        public HomegameEditPageModel GetEditModel(string slug, HomegameEditPostModel postModel)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            return _homegameEditPageModelFactory.Create(user, homegame, postModel);
        }

        public JoinHomegamePageModel GetJoinModel(string slug, JoinHomegamePostModel postModel)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            return _joinHomegamePageModelFactory.Create(user, homegame, postModel);
        }

        public JoinHomegameConfirmationPageModel GetJoinConfirmationModel(string slug)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            return _joinHomegameConfirmationPageModelFactory.Create(user, homegame);
        }
    }
}