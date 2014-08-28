using Core.Entities;
using Core.Repositories;
using Web.Models.PlayerModels.Add;

namespace Web.Commands.PlayerCommands
{
    public class AddPlayerCommand : Command
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly Bunch _bunch;
        private readonly AddPlayerPostModel _model;

        public AddPlayerCommand(IPlayerRepository playerRepository, Bunch bunch, AddPlayerPostModel model)
        {
            _playerRepository = playerRepository;
            _bunch = bunch;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            var existingPlayer = _playerRepository.GetByName(_bunch, _model.Name);
            if (existingPlayer != null)
            {
                AddError("The Display Name is in use by someone else");
                return false;
            }
            _playerRepository.Add(_bunch, _model.Name);
            return true;
        }
    }
}