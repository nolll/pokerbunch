using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;

namespace Web.Commands.HomegameCommands
{
    public class AddHomegameCommand : Command
    {
        private readonly IHomegameModelMapper _homegameModelMapper;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IUserContext _userContext;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISlugGenerator _slugGenerator;
        private readonly AddHomegamePostModel _postModel;

        public AddHomegameCommand(
            IHomegameModelMapper homegameModelMapper,
            IHomegameRepository homegameRepository,
            IUserContext userContext,
            IPlayerRepository playerRepository,
            ISlugGenerator slugGenerator,
            AddHomegamePostModel postModel)
        {
            _homegameModelMapper = homegameModelMapper;
            _homegameRepository = homegameRepository;
            _userContext = userContext;
            _playerRepository = playerRepository;
            _slugGenerator = slugGenerator;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            if (HomegameExists())
            {
                AddError("The Homegame name is not available");
                return false;
            }
            var homegame = _homegameModelMapper.GetHomegame(_postModel);
            homegame = _homegameRepository.AddHomegame(homegame);
            var user = _userContext.GetUser();
            _playerRepository.AddPlayerWithUser(homegame, user, Role.Manager);
            return true;
        }

        private bool HomegameExists()
        {
            var slug = _slugGenerator.GetSlug(_postModel.DisplayName);
            var homegame = _homegameRepository.GetByName(slug);
            return homegame != null;
        }
		
    }
}