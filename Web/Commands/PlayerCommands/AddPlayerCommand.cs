using System.ComponentModel.DataAnnotations;
using Core.Classes;
using Core.Repositories;
using Web.Models.PlayerModels.Add;

namespace Web.Commands.PlayerCommands
{
    public class AddPlayerCommand : Command
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly Homegame _homegame;
        private readonly AddPlayerPostModel _model;

        public AddPlayerCommand(IPlayerRepository playerRepository, Homegame homegame, AddPlayerPostModel model)
        {
            _playerRepository = playerRepository;
            _homegame = homegame;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            var existingPlayer = _playerRepository.GetByName(_homegame, _model.Name);
            if (existingPlayer != null)
            {
                AddError("The Display Name is in use by someone else");
                return false;
            }
            _playerRepository.AddPlayer(_homegame, _model.Name);
            return true;
        }
    }
}