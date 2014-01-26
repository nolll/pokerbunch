using Core.Classes;
using Core.Repositories;
using Infrastructure.Factories;
using Web.ModelMappers;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Report;

namespace Web.Commands.CashgameCommands
{
    public class CashgameCommandProvider : ICashgameCommandProvider
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameFactory _cashgameFactory;
        private readonly ICashgameModelMapper _cashgameModelMapper;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICheckpointModelMapper _checkpointModelMapper;
        private readonly ICheckpointRepository _checkpointRepository;

        public CashgameCommandProvider(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            ICashgameFactory cashgameFactory,
            ICashgameModelMapper cashgameModelMapper,
            IPlayerRepository playerRepository,
            ICheckpointModelMapper checkpointModelMapper,
            ICheckpointRepository checkpointRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _cashgameFactory = cashgameFactory;
            _cashgameModelMapper = cashgameModelMapper;
            _playerRepository = playerRepository;
            _checkpointModelMapper = checkpointModelMapper;
            _checkpointRepository = checkpointRepository;
        }

        public Command GetEndGameCommand(string slug)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            return new EndGameCommand(_cashgameRepository, homegame, cashgame);
        }

        public Command GetAddCommand(string slug, AddCashgamePostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);
            return new AddCashgameCommand(_cashgameRepository, _cashgameFactory, homegame, postModel);
        }

        public Command GetEditCommand(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            return new EditCashgameCommand(_homegameRepository, _cashgameRepository, _cashgameModelMapper, slug, dateStr, postModel);
        }

        public Command GetBuyinCommand(string slug, string playerName, BuyinPostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            return new BuyinCommand(_checkpointModelMapper, _checkpointRepository, _cashgameRepository, player, runningGame, postModel);
        }

        public Command GetReportCommand(string slug, string playerName, ReportPostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            var player = _playerRepository.GetByName(homegame, playerName);
            return new ReportCommand(_checkpointModelMapper, _checkpointRepository, cashgame, player, postModel);
        }

        public Command GetDeleteCheckpointCommand(string slug, string dateStr, int checkpointId)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            return new DeleteCheckpointCommand(_checkpointRepository, cashgame, checkpointId);
        }

        public Command GetCashoutCommand(string slug, string playerName, CashoutPostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var result = runningGame.GetResult(player.Id);
            return new CashoutCommand(_checkpointRepository, _checkpointModelMapper, runningGame, player, result, postModel);
        }

        public Command GetDeleteCommand(string slug, string dateStr)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            return new DeleteCommand(_cashgameRepository, cashgame);
        }
    }

    public class DeleteCommand : Command
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly Cashgame _cashgame;

        public DeleteCommand(
            ICashgameRepository cashgameRepository,
            Cashgame cashgame)
        {
            _cashgameRepository = cashgameRepository;
            _cashgame = cashgame;
        }

        public override bool Execute()
        {
            if (_cashgame.PlayerCount > 0)
            {
                AddError("Cashgames with results can't be deleted.");
                return false;
            }
            _cashgameRepository.DeleteGame(_cashgame);
            return true;
        }
    }
}