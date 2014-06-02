using Application.Services;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Details;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Security;

namespace Web.ModelServices
{
    public class HomegameModelService : IHomegameModelService
    {
        private readonly IAuth _auth;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IHomegameDetailsPageModelFactory _homegameDetailsPageModelFactory;
        private readonly IAddHomegamePageModelFactory _addHomegamePageModelFactory;
        private readonly IAddHomegameConfirmationPageModelFactory _addHomegameConfirmationPageModelFactory;
        private readonly IHomegameEditPageModelFactory _homegameEditPageModelFactory;
        private readonly IJoinHomegamePageModelFactory _joinHomegamePageModelFactory;
        private readonly IJoinHomegameConfirmationPageModelFactory _joinHomegameConfirmationPageModelFactory;

        public HomegameModelService(
            IAuth auth,
            IHomegameRepository homegameRepository,
            IHomegameDetailsPageModelFactory homegameDetailsPageModelFactory,
            IAddHomegamePageModelFactory addHomegamePageModelFactory,
            IAddHomegameConfirmationPageModelFactory addHomegameConfirmationPageModelFactory,
            IHomegameEditPageModelFactory homegameEditPageModelFactory,
            IJoinHomegamePageModelFactory joinHomegamePageModelFactory,
            IJoinHomegameConfirmationPageModelFactory joinHomegameConfirmationPageModelFactory)
        {
            _auth = auth;
            _homegameRepository = homegameRepository;
            _homegameDetailsPageModelFactory = homegameDetailsPageModelFactory;
            _addHomegamePageModelFactory = addHomegamePageModelFactory;
            _addHomegameConfirmationPageModelFactory = addHomegameConfirmationPageModelFactory;
            _homegameEditPageModelFactory = homegameEditPageModelFactory;
            _joinHomegamePageModelFactory = joinHomegamePageModelFactory;
            _joinHomegameConfirmationPageModelFactory = joinHomegameConfirmationPageModelFactory;
        }

        public HomegameDetailsPageModel GetDetailsModel(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var isInManagerMode = _auth.IsInRole(slug, Role.Manager);
            return _homegameDetailsPageModelFactory.Create(homegame, isInManagerMode);
        }

        public AddHomegamePageModel GetAddModel(AddHomegamePostModel postModel)
        {
            return _addHomegamePageModelFactory.Create(postModel);
        }

        public AddHomegameConfirmationPageModel GetAddConfirmationModel()
        {
            return _addHomegameConfirmationPageModelFactory.Create();
        }

        public HomegameEditPageModel GetEditModel(string slug, HomegameEditPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _homegameEditPageModelFactory.Create(homegame, postModel);
        }

        public JoinHomegamePageModel GetJoinModel(string slug, JoinHomegamePostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _joinHomegamePageModelFactory.Create(homegame, postModel);
        }

        public JoinHomegameConfirmationPageModel GetJoinConfirmationModel(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _joinHomegameConfirmationPageModelFactory.Create(homegame);
        }
    }
}