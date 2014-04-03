using Application.Services;
using Core.Classes;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;
using Web.Security;

namespace Web.Commands.HomegameCommands
{
    public class AddHomegameCommand : Command
    {
        private readonly IHomegameModelMapper _homegameModelMapper;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISlugGenerator _slugGenerator;
        private readonly AddHomegamePostModel _postModel;

        public AddHomegameCommand(
            IHomegameModelMapper homegameModelMapper,
            IHomegameRepository homegameRepository,
            IAuth auth,
            IPlayerRepository playerRepository,
            ISlugGenerator slugGenerator,
            AddHomegamePostModel postModel)
        {
            _homegameModelMapper = homegameModelMapper;
            _homegameRepository = homegameRepository;
            _auth = auth;
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
            homegame = _homegameRepository.Add(homegame);
            var user = _auth.GetUser();
            _playerRepository.Add(homegame, user, Role.Manager);
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