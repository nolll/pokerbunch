using Application.Services;
using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;

namespace Web.Commands.HomegameCommands
{
    public class AddHomegameCommand : Command
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;
        private readonly AddBunchPostModel _postModel;

        public AddHomegameCommand(
            IBunchRepository bunchRepository,
            IAuth auth,
            IPlayerRepository playerRepository,
            AddBunchPostModel postModel)
        {
            _bunchRepository = bunchRepository;
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
            homegame = _bunchRepository.Add(homegame);
            var user = _auth.CurrentUser;
            _playerRepository.Add(homegame, user, Role.Manager);
            return true;
        }

        private bool HomegameExists()
        {
            var slug = SlugGenerator.GetSlug(_postModel.DisplayName);
            var homegame = _bunchRepository.GetBySlug(slug);
            return homegame != null;
        }
		
    }
}