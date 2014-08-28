using Application.Services;
using Core.Entities;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;

namespace Web.Commands.HomegameCommands
{
    public class AddBunchCommand : Command
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;
        private readonly AddBunchPostModel _postModel;

        public AddBunchCommand(
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
            if (BunchExists())
            {
                AddError("The Homegame name is not available");
                return false;
            }
            var homegame = BunchModelMapper.GetBunch(_postModel);
            homegame = _bunchRepository.Add(homegame);
            var user = _auth.CurrentUser;
            _playerRepository.Add(homegame, user, Role.Manager);
            return true;
        }

        private bool BunchExists()
        {
            var slug = SlugGenerator.GetSlug(_postModel.DisplayName);
            var homegame = _bunchRepository.GetBySlug(slug);
            return homegame != null;
        }
		
    }
}