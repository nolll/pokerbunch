using Application.Services;
using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;

namespace Web.Commands.HomegameCommands
{
    public class AddHomegameCommand : Command
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;
        private readonly AddHomegamePostModel _postModel;

        public AddHomegameCommand(
            IHomegameRepository homegameRepository,
            IAuth auth,
            IPlayerRepository playerRepository,
            AddHomegamePostModel postModel)
        {
            _homegameRepository = homegameRepository;
            _auth = auth;
            _playerRepository = playerRepository;
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
            var homegame = HomegameModelMapper.GetHomegame(_postModel);
            homegame = _homegameRepository.Add(homegame);
            var user = _auth.CurrentUser;
            _playerRepository.Add(homegame, user, Role.Manager);
            return true;
        }

        private bool HomegameExists()
        {
            var slug = SlugGenerator.GetSlug(_postModel.DisplayName);
            var homegame = _homegameRepository.GetBySlug(slug);
            return homegame != null;
        }
		
    }
}