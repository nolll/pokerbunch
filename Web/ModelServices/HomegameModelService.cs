using Core.Repositories;
using Web.ModelFactories.HomegameModelFactories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;

namespace Web.ModelServices
{
    public class HomegameModelService : IHomegameModelService
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAddHomegamePageModelFactory _addHomegamePageModelFactory;
        private readonly IAddHomegameConfirmationPageModelFactory _addHomegameConfirmationPageModelFactory;
        private readonly IHomegameEditPageModelFactory _homegameEditPageModelFactory;
        private readonly IJoinHomegamePageModelFactory _joinHomegamePageModelFactory;
        private readonly IJoinHomegameConfirmationPageModelFactory _joinHomegameConfirmationPageModelFactory;

        public HomegameModelService(
            IHomegameRepository homegameRepository,
            IAddHomegamePageModelFactory addHomegamePageModelFactory,
            IAddHomegameConfirmationPageModelFactory addHomegameConfirmationPageModelFactory,
            IHomegameEditPageModelFactory homegameEditPageModelFactory,
            IJoinHomegamePageModelFactory joinHomegamePageModelFactory,
            IJoinHomegameConfirmationPageModelFactory joinHomegameConfirmationPageModelFactory)
        {
            _homegameRepository = homegameRepository;
            _addHomegamePageModelFactory = addHomegamePageModelFactory;
            _addHomegameConfirmationPageModelFactory = addHomegameConfirmationPageModelFactory;
            _homegameEditPageModelFactory = homegameEditPageModelFactory;
            _joinHomegamePageModelFactory = joinHomegamePageModelFactory;
            _joinHomegameConfirmationPageModelFactory = joinHomegameConfirmationPageModelFactory;
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