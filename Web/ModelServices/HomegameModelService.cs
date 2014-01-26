using App.Services.Interfaces;
using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Details;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Models.HomegameModels.List;

namespace Web.ModelServices
{
    public class HomegameModelService : IHomegameModelService
    {
        private readonly IAuthentication _authentication;
        private readonly IAuthorization _authorization;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IHomegameListPageModelFactory _homegameListPageModelFactory;
        private readonly IHomegameDetailsPageModelFactory _homegameDetailsPageModelFactory;
        private readonly IAddHomegamePageModelFactory _addHomegamePageModelFactory;
        private readonly IAddHomegameConfirmationPageModelFactory _addHomegameConfirmationPageModelFactory;
        private readonly IHomegameEditPageModelFactory _homegameEditPageModelFactory;
        private readonly IJoinHomegamePageModelFactory _joinHomegamePageModelFactory;
        private readonly IJoinHomegameConfirmationPageModelFactory _joinHomegameConfirmationPageModelFactory;

        public HomegameModelService(
            IAuthentication authentication,
            IAuthorization authorization,
            IHomegameRepository homegameRepository,
            IHomegameListPageModelFactory homegameListPageModelFactory,
            IHomegameDetailsPageModelFactory homegameDetailsPageModelFactory,
            IAddHomegamePageModelFactory addHomegamePageModelFactory,
            IAddHomegameConfirmationPageModelFactory addHomegameConfirmationPageModelFactory,
            IHomegameEditPageModelFactory homegameEditPageModelFactory,
            IJoinHomegamePageModelFactory joinHomegamePageModelFactory,
            IJoinHomegameConfirmationPageModelFactory joinHomegameConfirmationPageModelFactory)
        {
            _authentication = authentication;
            _authorization = authorization;
            _homegameRepository = homegameRepository;
            _homegameListPageModelFactory = homegameListPageModelFactory;
            _homegameDetailsPageModelFactory = homegameDetailsPageModelFactory;
            _addHomegamePageModelFactory = addHomegamePageModelFactory;
            _addHomegameConfirmationPageModelFactory = addHomegameConfirmationPageModelFactory;
            _homegameEditPageModelFactory = homegameEditPageModelFactory;
            _joinHomegamePageModelFactory = joinHomegamePageModelFactory;
            _joinHomegameConfirmationPageModelFactory = joinHomegameConfirmationPageModelFactory;
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
            var isInManagerMode = _authorization.IsInRole(homegame, Role.Manager);
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